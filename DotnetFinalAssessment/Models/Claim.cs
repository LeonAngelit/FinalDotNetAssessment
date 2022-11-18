using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DotnetFinalAssessment.Models
{
    public class Claim
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Description")]
        [StringLength(45)]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Status")]
        [StringLength(45)]
        public string Status { get; set; }
        [Required]
        [Display(Name = "Brand")]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        [Display(Name = "Vehicle Id")]
      
        public int VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

    }
}
