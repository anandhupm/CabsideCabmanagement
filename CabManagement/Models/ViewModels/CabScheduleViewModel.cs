using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CabManagement.Models.ViewModels
{
    public class CabScheduleViewModel
    {
        
        public IEnumerable<Location>? From { get; set; }
        public string FromLabel { get; set; }
        public string ToLabel { get; set; }

        public IEnumerable<Location>? To { get; set; }

        [DataType(DataType.Currency)] 
        public int Cost { get; set; }


    }
}
