namespace TrackR_API.Models.ResponseModel
{
    public class Identity
    {
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public object Actor { get; set; } // Adjust type as needed
        public object BootstrapContext { get; set; } // Adjust type as needed
        public List<Claim> Claims { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public string NameClaimType { get; set; }
        public string RoleClaimType { get; set; }
    }
}
