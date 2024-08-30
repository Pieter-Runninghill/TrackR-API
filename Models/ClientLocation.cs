using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TrackR_API.Models
{
    public class ClientLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string ClientLocationName { get; set; }

        public string ClientAddress { get; set; }

        public double ClientLatitude { get; set; }
        
        public double clientLongitude { get; set; }

        public Client Client { get; set; }
    }
}