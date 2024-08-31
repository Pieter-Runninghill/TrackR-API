namespace TrackR_API.Models.ResponseModel
{
    public class EligibleResponse
    {
        public bool IsEligible { get; set; }
        public float ReimbursableDistance { get; set; }
        public float ReimbursableValue { get; set; }
    }
}
