namespace Newshore.api.Model
{
    public class FlightBase
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }

        public Guid Id { get; set; }

        public FlightBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
