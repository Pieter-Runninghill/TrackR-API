using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackR_API.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  

        public int UserId { get; set; } 

        public int ClientLocationId { get; set; }
   
        public string ClientName { get; set; }

        public DateTime CreatedAt { get; set; }

        public User User { get; set; }

        public ICollection<ClientLocation>? ClientLocation { get; set; }

        public ICollection<Trip>? Trips { get; set; }
    }
}