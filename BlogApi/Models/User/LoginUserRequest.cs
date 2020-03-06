using System.ComponentModel.DataAnnotations;

namespace Models.User
{
    public class LoginUserRequest
    {
        [Required(ErrorMessage = "Email not specified")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password not specified")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
