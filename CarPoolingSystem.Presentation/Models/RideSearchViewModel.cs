using CarPoolingSystem.BusinessLogic.Models.RideDtos;

namespace CarPoolingSystem.Presentation.Models
{
    public class RideSearchViewModel
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<RideDetailsDTO>? Rides { get; set; }
    }

}
