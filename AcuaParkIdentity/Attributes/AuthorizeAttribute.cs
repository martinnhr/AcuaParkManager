using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AcuaParkIdentity.Attributes
{
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Obtener el token JWT del header de autorización
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            // Validar el token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(context.HttpContext.RequestServices.GetService<IConfiguration>()["JwtSettings:Key"]);

            try
            {
                // Parámetros de validación del token
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = context.HttpContext.RequestServices.GetService<IConfiguration>()["JwtSettings:Issuer"],
                    ValidAudience = context.HttpContext.RequestServices.GetService<IConfiguration>()["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

                if (validatedToken is not JwtSecurityToken jwtToken || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // Verificar los roles del usuario
                var roles = Roles.Split(',');
                var userRoles = principal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // Verificar si el usuario tiene al menos uno de los roles requeridos
                if (!roles.Any(role => userRoles.Contains(role)))
                {
                    // Si no tiene el rol necesario, devolver un mensaje personalizado
                    context.Result = new JsonResult(new { message = "No tienes el rol necesario para acceder a este recurso" })
                    {
                        StatusCode = 403 // Código de estado Forbidden (403)
                    };
                    return;
                }
            }
            catch (SecurityTokenExpiredException)
            {
                context.Result = new UnauthorizedResult();  // Token ha expirado
                return;
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();  // Token inválido
                return;
            }
        }
    }
}
