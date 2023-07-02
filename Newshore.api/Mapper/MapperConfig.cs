using AutoMapper;
using Newshore.api.Model;
using Newshore.api.Model.External;
using Route = Newshore.api.Model.Route;


namespace Newshore.api.Mapper
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {

            CreateMap<RouteDto, Route>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.Origin, act => act.MapFrom(src => src.DepartureStation))
                .ForMember(dest => dest.Destination, act => act.MapFrom(src => src.ArrivalStation))
                .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                .ForMember(dest => dest.Transport, act => act.MapFrom(src => new Transport { FlightCarrier = src.FlightCarrier, FlightNumber= src.FlightNumber }));



        }
    }
}
