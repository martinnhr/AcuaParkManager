using System.ComponentModel.DataAnnotations;

namespace AcuaParkIdentity.Controllers.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public String Password { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public String ConfirmPassword { get; set; }
    }
}
