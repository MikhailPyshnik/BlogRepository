using System.ComponentModel.DataAnnotations;

namespace BlogApi.Api.Simple_API_for_Authentication
{
    public class AuthenticateModel
    {
        [Required]
        public string Email { get; set; }
        //public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
