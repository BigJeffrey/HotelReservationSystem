using Microsoft.EntityFrameworkCore;
using Npgsql;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx)
        {
            context.Response.StatusCode = pgEx.SqlState switch
            {
                "23503" => StatusCodes.Status400BadRequest,
                "23505" => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status400BadRequest
            };

            await context.Response.WriteAsJsonAsync(new
            {
                message = pgEx.SqlState switch
                {
                    "23503" => "Invalid reference: related entity not found.",
                    "23505" => "Duplicate value violates a unique constraint.",
                    _ => "A database constraint was violated."
                },
                constraint = pgEx.ConstraintName
            });
        }
        catch (InvalidOperationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { message = "An unexpected error occurred." });
        }
    }
}
