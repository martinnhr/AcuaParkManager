using AcuaParkIdentity.Controllers.Roles.Models;
using AcuaParkIdentity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AcuaParkIdentity.Controllers.Roles
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly RoleManager<MyRol> _roleManager;

        public RolController(RoleManager<MyRol> roleManager)
        {
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("newRole")]
        public async Task<IActionResult> NewRole([FromBody] NewRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = new MyRol { Name = model.Name };

            var res = await _roleManager.CreateAsync(role);

            return Ok(res);

        }

        


    }
}
