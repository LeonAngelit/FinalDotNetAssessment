using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
        [Column("Vehicle_Id")]
        [Display(Name = "VIN")]

      
        public int VehicleId { get; set; }
        [JsonIgnore]
        public virtual Vehicle? Vehicle { get; set; }

    }
}
