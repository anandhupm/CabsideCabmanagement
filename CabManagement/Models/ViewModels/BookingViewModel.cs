namespace CabManagement.Models.ViewModels
{
    public class BookingViewModel
    {
        public IEnumerable<Location>? Pickup { get; set; }
        public string PickupLabel { get; set; }
        public string DestinationLabel { get; set; }

        public IEnumerable<Location>? Destination { get; set; }
    }
}
