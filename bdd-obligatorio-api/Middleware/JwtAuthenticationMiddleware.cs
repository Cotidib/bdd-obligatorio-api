using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace bdd_obligatorio_api.Middleware
{
    public class JwtAuthenticationMiddleware : IMiddleware
    {
        private IConfiguration _config;

        public JwtAuthenticationMiddleware(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var path = context.Request.Path.Value;

            if (path.Equals("/login") || path.Equals("/register"))
            {
                Console.WriteLine("Se está accediendo a", path);
                await next.Invoke(context);
                return;
            }

            var authHeader = context.Request.Headers["Authorization"];
            Console.WriteLine(authHeader);
            var token = authHeader.ToString().Split(' ')[1];
            Console.WriteLine("Token sacado por Invoke:", token);
            if (token == null)
            {
                context.Response.StatusCode = 401;
                return;
            }

            try
            {
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]); // Reemplaza con tu clave secreta
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // Puedes agregar otras validaciones según tus necesidades
                };

                SecurityToken validatedToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                // Aquí puedes acceder a la información del usuario a través de 'principal'
                context.Items["user"] = principal;

                // Mostrar información en la consola
                Console.WriteLine("Nombre de usuario: " + principal.Identity.Name);

                await next.Invoke(context);
            }
            catch (SecurityTokenException ex)
            {
                Console.WriteLine($"Error de validación del token: {ex.Message}");
                context.Response.StatusCode = 403;
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error durante la autenticación del token: {ex.Message}");
                context.Response.StatusCode = 500;
                return;
            }
        }
    }
}
