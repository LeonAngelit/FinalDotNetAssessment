using System.Text.Json.Serialization;

namespace DotnetFinalAssessment.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Vin { get; set; }
        public string Color { get; set; }
        public int Date { get; set; } = DateTime.Now.Year;
        public int OwnerId{ get; set; }
        public virtual Owner Owner { get; set; }

        [JsonIgnore]
        public virtual ICollection<Claim> Claims{ get; set; }
    }
}
