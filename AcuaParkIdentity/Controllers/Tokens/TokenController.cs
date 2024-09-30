using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AcuaParkIdentity.Controllers.Tokens
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Endpoint para verificar si el token es válido y no ha expirado
        [HttpGet("VerifyToken")]
        public IActionResult VerifyToken()
        {
            // Obtener el token JWT del header de autorización
            var authorizationHeader = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return Unauthorized(new { message = "Token no proporcionado o malformado" });
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, // Esto asegura que el token no esté caducado
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidAudience = _configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero  // Evitar tiempos adicionales para la expiración
                };

                // Validar el token
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                // Si el token es válido, devolver una respuesta positiva
                return Ok(new { message = "Token válido" });
            }
            catch (SecurityTokenExpiredException)
            {
                // Token ha expirado
                return Unauthorized(new { message = "El token ha expirado" });
            }
            catch (SecurityTokenException)
            {
                // Token inválido
                return Unauthorized(new { message = "El token es inválido" });
            }
            catch (Exception)
            {
                // Cualquier otro error
                return Unauthorized(new { message = "Error al validar el token" });
            }
        }
    }
}

