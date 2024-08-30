using System.Text.RegularExpressions;

namespace TrackR_API.Utils
{
    public class EmailValidator
    {
        public bool ValidateEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            return emailRegex.IsMatch(email);
        }
    }
}
