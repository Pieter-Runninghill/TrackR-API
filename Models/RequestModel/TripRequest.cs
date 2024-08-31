namespace TrackR_API.Models.RequestModel
{
    public class TripRequest
    {
        public int UserId { get; set; }

        public int ClientId { get; set; }

        public DateTime TripDate { get; set; }

        public string StartAddress { get; set; }

        public double StartLongitude { get; set; }

        public double StartLatitude { get; set; }

        public string EndAddress { get; set; }

        public double EndLongitude { get; set; }

        public double EndLatitude { get; set; }
    }
}
