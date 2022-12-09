using System.ComponentModel.DataAnnotations;

namespace CabManagement.Models.ViewModels
{
    public class CabDetailsViewModel
    {
        [Required]
        [Display(Name = "Vehicle Number")]
        [MaxLength(20)]
        
        public string VehicleNumber { get; set; }
        [Display(Name = "Name")]
        [MaxLength(20)]

        [Required]
        public string Name { get; set; }

        [Display(Name = "Model Number")]
        [MaxLength(20)]

        [Required]
        public string ModelNumber { get; set; }

        [Required]

        public CabType Type { get; set; }
        [Required]

        public SeatingCapacity Capacity { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
    }
}
