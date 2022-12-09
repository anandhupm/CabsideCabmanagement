using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagement.Models
{
    public enum CabType
    {
        Auto,
        Bus,
        Bike,
        Car,
        Van

    }
    public enum SeatingCapacity
    {
        [Display(Name = "3 Seat")]
        ThreeSeat,
        [Display(Name = "4 Seat")]
        FourSeat,
        [Display(Name = "5 Seat")]
        FiveSeat,
        [Display(Name = "6 Seat")]
        SixSeat,
        [Display(Name = "10 Seat")]
        TenSeat,
        [Display(Name = "Below 20 Seat")]
        TwentySeat,
        [Display(Name = "Below 50 Seat")]
        FiftySeat,
        [Display(Name = "Below 60 Seat")]
        SixtyySeat,

    }

    public class CabDetail
    {
        public int Id { get; set; }

        [Display(Name ="Vehicle Number")]
        [MaxLength(20)]
        public string VehicleNumber { get; set; }

        [Display(Name = "Name")]
        [MaxLength(20)]
        public string Name { get; set; }


        [Display(Name = "Model Number")]
        [MaxLength(20)]
        public string ModelNumber { get; set; }

        public CabType Type { get; set; }

        public SeatingCapacity Capacity { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

    }
}
