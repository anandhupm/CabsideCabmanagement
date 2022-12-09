using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagement.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        
        public int CabScheduleId { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
