using AcuaParkIdentity.Attributes;
using AcuaParkIdentity.Controllers.Users.Models;
using AcuaParkIdentity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace AcuaParkIdentity.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<MyUser> _userManager;
        private readonly SignInManager<MyUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<MyUser> userManager, SignInManager<MyUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }




        // Endpoint para registrar usuarios
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maxIdNum = await _userManager.Users.MaxAsync(u => (int?)u.idNum) ?? 0;
            var newIdNum = maxIdNum + 1;

            var user = new MyUser
            {
                UserName = model.Email,
                Email = model.Email,
                idNum = newIdNum,

            };
            
            var result = await _userManager.CreateAsync(user, model.Password);
            var res = await _userManager.AddToRoleAsync(user, "ADMIN");
            var res2 = await _userManager.AddToRoleAsync(user, "string");

            if (result.Succeeded)
            {
                return Ok(new { 
                    result,
                    //res,
                    message = "User registered successfully" 
                });
            }

            // Si hubo errores, devolverlos
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }


        [Authorize(Roles = "string")]
        [HttpDelete("DeleteUserAutomaticoString/{userId}")]
        public async Task<IActionResult> DeleteUserAutomaticoString(int userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.idNum == userId);

            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Usuario eliminado exitosamente" });
        }



        [Authorize(Roles = "ADMIN")]
        [HttpDelete("DeleteUserAutomatico/{userId}")]
        public async Task<IActionResult> DeleteUserAutomatico(int userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.idNum == userId);

            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Usuario eliminado exitosamente" });
        }

        [HttpPut("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserModel model)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.idNum == userId);

            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }

            // Actualizar los campos necesarios
            /*user.Email = model.Email;
            user.UserName = model.UserName;*/
            // Puedes agregar otros campos aquí según sea necesario

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Usuario actualizado exitosamente" });
        }


        // Endpoint para logear usuario
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Verifica las credenciales del usuario
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                // Busca el usuario autenticado
                var user = await _userManager.FindByEmailAsync(model.Email);

                // Genera el token JWT
                var token = await GenerateJwtToken(user);

                return Ok(new { token });
            }
            else
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }
        }

        // Método para generar el JWT
        private async Task<string> GenerateJwtToken(MyUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);  // Obtener roles del usuario
            
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName)
    };

            // Agregar los roles como claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}