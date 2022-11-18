using System.Text.Json.Serialization;

namespace DotnetFinalAssessment.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DriverLicense { get; set; }

        [JsonIgnore]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
