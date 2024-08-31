namespace TrackR_API.Models.RequestModel
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
