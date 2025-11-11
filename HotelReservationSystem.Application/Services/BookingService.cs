using HotelReservationSystem.Application.DTOs.Bookings;
using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Application.Interfaces.Services;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<PagedResponse<BookingResponse>> GetAllAsync(int page, int pageSize)
        {
            var totalCount = await _bookingRepository.CountAsync();

            var bookings = await _bookingRepository.GetAllAsync(page, pageSize);

            var items = bookings.Select(b => new BookingResponse
            {
                BookingId = b.BookingId,
                BookingDate = b.BookingDate,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Status = b.Status,
                Customer = new CustomerResponse
                {
                    CustomerId = b.Customer.CustomerId,
                    FirstName = b.Customer.FirstName,
                    LastName = b.Customer.LastName,
                    Email = b.Customer.Email,
                    PhoneNumber = b.Customer.PhoneNumber
                },
                BookingDetails = b.BookingDetails.Select(d => new BookingDetailResponse
                {
                    Nights = d.Nights,
                    Price = d.Price,
                    Room = new RoomResponse
                    {
                        RoomId = d.Room.RoomId,
                        RoomNumber = d.Room.RoomNumber,
                        Type = d.Room.RoomType,
                        PricePerNight = d.Room.PricePerNight,
                        Capacity = d.Room.Capacity,
                        IsAvailable = d.Room.IsAvailable
                    }
                }).ToList(),
                Payments = b.Payments.Select(p => new PaymentResponse
                {
                    PaymentId = p.PaymentId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    Status = p.Status
                }).ToList(),
                ExtraServices = b.BookingServices.Select(s => new BookingServiceResponse
                {
                    TotalPrice = s.TotalPrice,
                    Quantity = s.Quantity,
                    ExtraService = new ExtraServiceResponse
                    {
                        ExtraServiceId = s.ExtraService.ExtraServiceId,
                        Name = s.ExtraService.Name,
                        Description = s.ExtraService.Description,
                        Price = s.ExtraService.Price
                    }
                }).ToList()
            }).ToList();

            return new PagedResponse<BookingResponse>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<BookingResponse?> GetByIdAsync(int id)
        {
            var b = await _bookingRepository.GetByIdAsync(id);
            if (b is null)
                return null;

            var bookingResponse = new BookingResponse
            {
                BookingId = b.BookingId,
                BookingDate = b.BookingDate,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                Status = b.Status,
                Customer = new CustomerResponse
                {
                    CustomerId = b.Customer.CustomerId,
                    FirstName = b.Customer.FirstName,
                    LastName = b.Customer.LastName,
                    Email = b.Customer.Email,
                    PhoneNumber = b.Customer.PhoneNumber
                },
                BookingDetails = b.BookingDetails.Select(d => new BookingDetailResponse
                {
                    Nights = d.Nights,
                    Price = d.Price,
                    Room = new RoomResponse
                    {
                        RoomId = d.Room.RoomId,
                        RoomNumber = d.Room.RoomNumber,
                        Type = d.Room.RoomType,
                        PricePerNight = d.Room.PricePerNight,
                        Capacity = d.Room.Capacity,
                        IsAvailable = d.Room.IsAvailable
                    }
                }).ToList(),
                Payments = b.Payments.Select(p => new PaymentResponse
                {
                    PaymentId = p.PaymentId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    Status = p.Status
                }).ToList(),
                ExtraServices = b.BookingServices.Select(s => new BookingServiceResponse
                {
                    TotalPrice = s.TotalPrice,
                    Quantity = s.Quantity,
                    ExtraService = new ExtraServiceResponse
                    {
                        ExtraServiceId = s.ExtraService.ExtraServiceId,
                        Name = s.ExtraService.Name,
                        Description = s.ExtraService.Description,
                        Price = s.ExtraService.Price
                    }
                }).ToList()
            };

            return bookingResponse;
        }

        public async Task<Booking> AddAsync(CreateBookingRequest request)
        {
            if (request.StartDate < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Start date cannot be in the past.");
            }

            if (request.EndDate < DateTime.UtcNow)
            {
                throw new InvalidOperationException("End date cannot be in the past.");
            }

            if (request.StartDate >= request.EndDate)
            {
                throw new InvalidOperationException("Start date must be earlier than end date.");
            }

            Booking newBooking = new()
            {
                CustomerId = request.CustomerId,
                BookingDate = DateTime.UtcNow,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status
            };

            Booking createdBooking = await _bookingRepository.AddAsync(newBooking);
            await _bookingRepository.SaveChangesAsync();

            return createdBooking;
        }

        public async Task<Booking?> UpdateAsync(int id, UpdateBookingRequest request)
        {
            var BookingToUpdate = await _bookingRepository.GetByIdAsync(id);
            if (BookingToUpdate == null)
                return null;

            if (!string.IsNullOrWhiteSpace(request.Status))
                BookingToUpdate.Status = request.Status;


            if (request.StartDate < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Start date cannot be in the past.");
            }

            if (request.EndDate < DateTime.UtcNow)
            {
                throw new InvalidOperationException("End date cannot be in the past.");
            }

            if (request.StartDate < request.EndDate)
            {
                BookingToUpdate.StartDate = request.StartDate;
                BookingToUpdate.EndDate = request.EndDate;
            }
            else
            {
                throw new InvalidOperationException("Start date must be earlier than end date.");
            }

            var udpated = await _bookingRepository.UpdateAsync(BookingToUpdate);
            await _bookingRepository.SaveChangesAsync();

            return udpated;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _bookingRepository.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Booking not found.");

            await _bookingRepository.DeleteAsync(id);
            await _bookingRepository.SaveChangesAsync();
        }
    }
}
