using Newshore.api.Model;
using Route = Newshore.api.Model.Route;

namespace Newshore.api.Business
{
    public interface IRouteService
    {

        Task LoadRoutesAsync();
        Task<List<Route>> GetRoutes();
        Task<List<Journey>> FindAllRoutes(string origin, string destination);
    }
}
