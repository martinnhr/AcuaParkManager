using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AcuaParkIdentity.Controllers.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

    }
}
