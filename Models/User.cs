using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrackR_API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string HomeAddress { get; set; }

        public double HomeLongitude { get; set; }

        public double HomeLatitude { get; set; }

        public string OfficeAddress { get; set; }

        public double OfficeLongitude { get; set; }
        
        public double OfficeLatitude { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public ICollection<Client>? Clients { get; set; }

        public ICollection<Trip>? Trips { get; set; }
    }
}
