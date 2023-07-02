namespace Newshore.api.Model.External
{
    public class RouteDto
    {
        public int Id { get; set; }
        public string? DepartureStation { get; set; }
        public string? ArrivalStation { get; set; }
        public string? FlightCarrier { get; set; }
        public string? FlightNumber { get; set; }
        public decimal? Price { get; set; }
    }
}
