using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Display(Name = "Date")]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
<<<<<<< HEAD
        [Display(Name = "Vehicle Id")]
        [Column("Vehicle_Id")]
=======
        [Display(Name = "VIN")]
>>>>>>> 8384642730900571d64075ac35f8b24a0d53e893
      
        public int VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

    }
}
