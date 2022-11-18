using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DotnetFinalAssessment.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(45)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(45)]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Driver License")]
        [StringLength(45)]
        public string DriverLicense { get; set; }
        [JsonIgnore]
        public virtual ICollection<Vehicle>? Vehicles { get; set; }
    }
}
