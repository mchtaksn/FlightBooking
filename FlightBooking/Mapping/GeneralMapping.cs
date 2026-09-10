using AutoMapper;
using FlightBooking.Dtos.FlightDtos;
using FlightBooking.Dtos.PassengerDtos;
using FlightBooking.Entities;

namespace FlightBooking.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Flight, CreateFlightDto>().ReverseMap();
            CreateMap<Flight, ResultFlightDto>().ReverseMap();
            CreateMap<Flight, UpdateFlightDto>().ReverseMap();
            CreateMap<Flight, GetFlightByIdDto>().ReverseMap();
            CreateMap<Flight, PassengerListItemDto>().ReverseMap();
        }
    }
}
