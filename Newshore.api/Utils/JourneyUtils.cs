using AutoMapper;
using Newshore.api.Model;
using Route = Newshore.api.Model.Route;

namespace Newshore.api.Utils
{
    public static class JourneyUtils
    {
        public static Journey MapToJourney(List<Route> routeList)
        {
            var flights = routeList.Select(route => new Flight
            {
                Origin= route.Origin,
                Destination = route.Destination,
                Transport = new Transport
                {
                    FlightCarrier = route.Transport.FlightCarrier,
                    FlightNumber = route.Transport.FlightNumber
                },
                Price= route.Price
            }).ToList();
            
            flights.Add(new Flight { Origin = routeList.Last().Destination});


            return new Journey
            {
                Flights = flights,
                Origin = routeList.First().Origin,
                Destination = routeList.Last().Destination,
                Price = routeList.Sum(route => route.Price),
                
            };
        }

        public static void FindLowestPrice(List<Journey> journeys)
        {
            if (journeys.Count() == 0) return;

            decimal lowestPrice = journeys.Min(trip => trip.Price);
            bool hasMultipleLowestPrice = journeys.Count(trip => trip.Price == lowestPrice) > 1;

            foreach (Journey journey in journeys)
            {
                journey.LowestPrice = journey.Price == lowestPrice && (!hasMultipleLowestPrice || journey.Price > 0);
            }
        }

    }
}
