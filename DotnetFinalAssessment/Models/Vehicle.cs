using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace DotnetFinalAssessment.Models
{
    public class Vehicle
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Brand")]
        [StringLength(45)]
        public string Brand { get; set; }
        [Required]
        [Display(Name = "Vin")]
        [StringLength(45)]
        public string Vin { get; set; }
        [Required]
        [Display(Name = "Color")]
        [StringLength(45)]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Date")]
     
        public int Date { get; set; } = DateTime.Now.Year;
        [Required]
        [Display(Name = "Owner Id")]
      
        public int OwnerId{ get; set; }
        public virtual Owner? Owner { get; set; }

        [JsonIgnore]
        public virtual ICollection<Claim>? Claims{ get; set; }
    }
}
