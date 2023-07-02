namespace Newshore.api.Model
{
    public class Journey : FlightBase
    {
        public List<Flight> Flights { get; set; }

        public bool LowestPrice { get; set; }

      

    }
}
