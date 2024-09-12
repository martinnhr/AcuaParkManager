using System.ComponentModel.DataAnnotations;

namespace AcuaParkIdentity.Controllers.Roles.Models
{
    public class NewRoleModel
    {
        [Required]
        public string Name { get; set; }
    }
}
