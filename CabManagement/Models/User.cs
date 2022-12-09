using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagement.Models
{
    public class User:IdentityUser
    {
        [StringLength(15)]
        public string FirstName { get; set; }

        [StringLength(15)]
        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        [StringLength(250)]
        public string Address { get; set; }
        
    }
}
