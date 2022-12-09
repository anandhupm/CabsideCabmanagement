using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagement.Models
{
    public enum Location
    {
        Thrissure,
        Kochi,
        Kollam,
        Kottayam,
        Kannur,
        Palakkad,
        Allapuzha,
        Rajakkad,
        Munnar,
        Idukki,
        Chennai

    }
    public class CabSchedule
    {
        public int CabScheduleId { get; set; }

        public Location From { get; set; }
        public Location To { get; set; }


        public int Cost { get; set; }

        public bool IsAvailable { get; set; }

        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }


    }
}
