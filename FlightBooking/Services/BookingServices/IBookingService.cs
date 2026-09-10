using FlightBooking.Dtos.BookingDtos;

namespace FlightBooking.Services.BookingServices
{
    public interface IBookingService
    {
        Task CreateBookingAsync(CreateBookingDto createBookingDto);
        Task<(string Name, string Surname)> GetPassengerNameByIdAsync(string passengerId);
        Task<string> GetPnrByPassengerIdAsync(string passengerId);
        Task<string> GetGateByPassengerIdAsync(string passengerId);
    }
}
