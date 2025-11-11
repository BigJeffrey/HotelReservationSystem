using HotelReservationSystem.Application.DTOs.Common;
using HotelReservationSystem.Application.DTOs.Customers;
using HotelReservationSystem.Application.Interfaces.Repositories;
using HotelReservationSystem.Application.Interfaces.Services;
using HotelReservationSystem.Domain.Entities;

namespace HotelReservationSystem.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<PagedResponse<CustomerResponse>> GetAllAsync(int page, int pageSize)
        {
            var totalCount = await _customerRepository.CountAsync();

            var customers = await _customerRepository.GetAllAsync(page, pageSize);

            var items = customers.Select(c => new CustomerResponse
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Bookings = c.Bookings.Select(b => new BookingsResponse
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    Status = b.Status
                }).ToList()
            }).ToList();

            return new PagedResponse<CustomerResponse>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<CustomerResponse?> GetByIdAsync(int id)
        {
            var c = await _customerRepository.GetByIdAsync(id);
            if (c == null)
                return null;

            return new CustomerResponse
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Bookings = c.Bookings.Select(b => new BookingsResponse
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    StartDate = b.StartDate,
                    EndDate = b.EndDate,
                    Status = b.Status
                }).ToList()
            };
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _customerRepository.GetByEmailAsync(email);
        }

        public async Task<Customer> AddAsync(CreateCustomerRequest customer)
        {
            var existing = await _customerRepository.GetByEmailAsync(customer.Email);
            if (existing != null)
                throw new InvalidOperationException("Customer with this email already exists.");

            Customer newCustomer = new()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };

            Customer createdCustomer = await _customerRepository.AddAsync(newCustomer);
            await _customerRepository.SaveChangesAsync();

            return createdCustomer;
        }

        public async Task<Customer?> UpdateAsync(int id, UpdateCustomerRequest request)
        {
            var customerToUpdate = await _customerRepository.GetByIdAsync(id);
            if (customerToUpdate == null)
                return null;

            if (!string.IsNullOrWhiteSpace(request.FirstName))
                customerToUpdate.FirstName = request.FirstName;

            if (!string.IsNullOrWhiteSpace(request.LastName))
                customerToUpdate.LastName = request.LastName;

            if (!string.IsNullOrWhiteSpace(request.Email) &&
                !string.Equals(request.Email, customerToUpdate.Email, StringComparison.OrdinalIgnoreCase))
            {
                var existingEmail = await _customerRepository.GetByEmailAsync(request.Email);
                if (existingEmail != null)
                    throw new InvalidOperationException("Customer with this email already exists.");

                customerToUpdate.Email = request.Email;
            }

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                customerToUpdate.PhoneNumber = request.PhoneNumber;

            var udpated = await _customerRepository.UpdateAsync(customerToUpdate);
            await _customerRepository.SaveChangesAsync();

            return udpated;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _customerRepository.GetByIdAsync(id);
            if (existing is null)
                throw new KeyNotFoundException("Customer not found.");

            await _customerRepository.DeleteAsync(id);
            await _customerRepository.SaveChangesAsync();
        }
    }
}
