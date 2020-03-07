using System.ComponentModel.DataAnnotations;

namespace Models.User
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "Email is required.")]
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        //[Display(Name = "Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
