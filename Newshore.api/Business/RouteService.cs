using AutoMapper;
using Newshore.api.Context;
using Newshore.api.Integration;
using Newshore.api.Model;
using Newshore.api.Model.External;
using Newshore.api.Utils;
using Route = Newshore.api.Model.Route;

namespace Newshore.api.Business
{
    public class RouteService: IRouteService
    {
        private readonly IHttpClientService<RouteDto> _httpClient;
        private readonly IMapper _mapper;
        private readonly RouteDb _dbContext;



        public RouteService(IHttpClientService<RouteDto> httpClient, IMapper mapper, RouteDb dbContext ) 
        {
             _httpClient= httpClient;
            _mapper= mapper;
            _dbContext = dbContext;
        }

        public async Task LoadRoutesAsync()
        {
            List<RouteDto> response = await _httpClient.GetAsync();
            _dbContext.Routes.AddRange(response.ToArray());
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Journey>> FindAllRoutes(string origin, string destination)
        {
            var allRoutes = new List<List<Route>>();
            List<Route> routes = await GetRoutes();

            FindRoutes(routes, origin, origin, destination, new List<Route>(), allRoutes);

            List<Journey> journeyList = allRoutes.Select(r => JourneyUtils.MapToJourney(r)).OrderBy(d => d.Price).ToList();

            JourneyUtils.FindLowestPrice(journeyList);

            return journeyList;
        }

        public async Task<List<Route>> GetRoutes()
        {
            List<RouteDto> response = _dbContext.Routes.ToList();

            List<Route> routes = _mapper.Map<List<RouteDto>, List<Route>>(response);

            return routes;

        }


        private void FindRoutes(List<Route> routes, string currentLocation, string origin ,string destination, List<Route> currentRoute, List<List<Route>> allRoutes)
        {

            if (currentLocation == destination)
            {
                // Se llegó al destino, se agrega la ruta actual a la lista de todas las rutas
                allRoutes.Add(new List<Route>(currentRoute));
                return;
            }

            var nextRoutes = routes.Where(r => r.Origin == currentLocation 
                                          && !currentRoute.Contains(r) 
                                          && currentRoute.Where(c=> c.Origin == r.Destination && c.Destination == r.Origin).Count() == 0
                                          && currentRoute.Where(c => c.Destination == origin).Count() == 0);


            foreach (var route in nextRoutes)
            {
                currentRoute.Add(route);
                FindRoutes(routes, route.Destination,origin, destination, currentRoute, allRoutes);
                currentRoute.Remove(route);
            }

        }
    }
}
