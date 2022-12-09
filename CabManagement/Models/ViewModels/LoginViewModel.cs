using System.ComponentModel.DataAnnotations;

namespace CabManagement.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [StringLength(25)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
