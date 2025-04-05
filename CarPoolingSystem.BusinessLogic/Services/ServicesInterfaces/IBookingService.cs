using CarPoolingSystem.BusinessLogic.Models.BookingDtos;
using CarPoolingSystem.DataAccess.Entites.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPoolingSystem.BusinessLogic.Services.ServicesInterfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDetailsDTO>> GetAllBookingsAsync();
        Task<BookingDetailsDTO?> GetBookingByIdAsync(int id);
        Task <bool> AddBookingAsync(CreateBookingDTO booking);
        Task<int> UpdateBookingAsync(UpdateBookingDTO booking);
        Task<bool> DeleteBookingAsync(int id);
    }
}
