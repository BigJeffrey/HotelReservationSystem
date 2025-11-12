using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Application.DTOs.ExtraServices;
using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Application.Interfaces.Services;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Services
{
    public class ExtraServiceService : IExtraServiceService
    {
        private readonly IExtraServiceRepository _extraServiceRepository;

        public ExtraServiceService(IExtraServiceRepository extraServiceRepository)
        {
            _extraServiceRepository = extraServiceRepository;
        }

        public async Task<PagedResponse<ExtraServiceResponse>> GetAllAsync(int page, int pageSize)
        {
            var totalCount = await _extraServiceRepository.CountAsync();

            var extraServices = await _extraServiceRepository.GetAllAsync(page, pageSize);

            var items = extraServices.Select(e => new ExtraServiceResponse(e)).ToList();

            return new PagedResponse<ExtraServiceResponse>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<ExtraServiceResponse?> GetByIdAsync(int id)
        {
            var e = await _extraServiceRepository.GetByIdAsync(id);
            if (e == null)
                return null;

            return new ExtraServiceResponse(e);
        }

        public async Task<ExtraService> AddAsync(CreateExtraServiceRequest request)
        {
            var existing = await _extraServiceRepository.GetByNameAsync(request.Name);
            if (existing != null)
                throw new InvalidOperationException("An extra service with this name already exists.");

            var newService = new ExtraService
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            var created = await _extraServiceRepository.AddAsync(newService);
            await _extraServiceRepository.SaveChangesAsync();

            return created;
        }

        public async Task<ExtraServiceResponse?> UpdateAsync(int id, UpdateExtraServiceRequest request)
        {
            var existing = await _extraServiceRepository.GetByIdAsync(id);
            if (existing is null)
                return null;

            if (!string.IsNullOrEmpty(request.Name) && !string.Equals(existing.Name, request.Name, StringComparison.OrdinalIgnoreCase))
            {
                var duplicate = await _extraServiceRepository.GetByNameAsync(request.Name);
                if (duplicate != null)
                    throw new InvalidOperationException("An extra service with this name already exists.");

                existing.Name = request.Name;
            }

            if (!string.IsNullOrEmpty(request.Description))
                existing.Description = request.Description;

            if (request.Price.HasValue)
                existing.Price = request.Price.Value;

            var e = await _extraServiceRepository.UpdateAsync(existing);
            await _extraServiceRepository.SaveChangesAsync();

            return new ExtraServiceResponse(e);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _extraServiceRepository.GetByIdAsync(id);
            if (existing is null)
                return;

            await _extraServiceRepository.DeleteAsync(id);
            await _extraServiceRepository.SaveChangesAsync();
        }
    }
}
