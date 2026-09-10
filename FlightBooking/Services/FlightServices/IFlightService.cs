using FlightBooking.Dtos.FlightDtos;
using FlightBooking.Dtos.PassengerDtos;

namespace FlightBooking.Services.FlightServices
{
    public interface IFlightService
    {
        Task<List<ResultFlightDto>> GetAllFlightAsync();
        Task<GetFlightByIdDto>GetFlightByIdAsync(string id);
        Task CreateFlightAsync(CreateFlightDto createFlightDto);
        Task DeleteFlightAsync(string id);
        Task UpdateFlightAsync(UpdateFlightDto updateFlightDto);
        Task<List<PassengerListItemDto>> GetFlightDetailsWithPassengers(string id);

    }
}
