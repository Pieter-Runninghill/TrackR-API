namespace TrackR_API.Models.ResponseModel
{
    public class IdentityContainerBase
    {
        public List<Claim> Claims { get; set; }
        public List<Identity> Identities { get; set; }
        public Identity Identity { get; set; }
    }
}