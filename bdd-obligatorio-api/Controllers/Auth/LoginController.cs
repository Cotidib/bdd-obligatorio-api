using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using bdd_obligatorio_api.Contracts.Login;
using bdd_obligatorio_api.Contracts.Register;
using bdd_obligatorio_api.Models;
using bdd_obligatorio_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace bdd_obligatorio_api.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IAuthService _authService;
    private IConfiguration _config;
    public LoginController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _config = configuration;
    }

    [Authorize]
    [HttpGet("/authmiddleware")]
    public IActionResult AuthMiddleware()
    {
        return Ok(new { Autenticado = true, Status = 200 });
    }

    [AllowAnonymous]
    [HttpPost("/register")]
    public IActionResult registerUser(RegisterRequest registerRequest)
    {
        try
        {
            var user = new User(
                registerRequest.Username,
                HashPassword(registerRequest.Password)
            );

            _authService.RegisterUser(user);

            var response = new RegisterResponse(
                registerRequest.Username,
                registerRequest.Password
            );

            return CreatedAtAction(
                actionName: nameof(getUser),
                routeValues: new { id = user.Username },
                value: response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = $"Error en la prueba: {ex.Message}" });
        }
    }
    [AllowAnonymous]
    [HttpPost("/login")]
    public async Task<IActionResult?> loginUser(LoginRequest loginRequest)
    {
        try
        {
            var user = await _authService.LoginUser(loginRequest.Username);
            if (user != null)
            {
                if (VerifyPassword(loginRequest.Password, user.Password)){
                    var tokenString = GenerateJSONWebToken(user);
                    Console.WriteLine(tokenString);
                    return Ok(new { token = tokenString });
                }
                else{
                    return NotFound(new { mensaje = $"Contraseña incorrecta para el user: {loginRequest.Username}" });

                }
            }
            else
            {
                return NotFound(new { mensaje = $"No se encontró el usuario: {loginRequest.Username}" });
            }
        }
        catch (SecurityTokenException ex)
        {
            Console.WriteLine($"Error de validación del token: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = $"Error en la prueba: {ex.Message}" });
        }
    }

    [HttpGet("{id}")]
    public IActionResult getUser(string username)
    {
        try
        {
            var user = _authService.GetUser(username);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound(new { mensaje = $"No se encontró el usuario: {username}" });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = $"Error en la prueba: {ex.Message}" });
        }
    }

    private string GenerateJSONWebToken(User userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userInfo.Username),
        };
        var token = new JwtSecurityToken
        (
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public IActionResult GetUserId()
    {
        try
        {
            var principal = HttpContext.User;

            var userIdClaim = principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                return Ok(new { UserId = userId });
            }
            else
            {
                return NotFound(new { mensaje = "No se encontró la claim de UserId en el token." });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = $"Error al obtener el UserId: {ex.Message}" });
        }
    }
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    private bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        return hashedPassword.Equals(HashPassword(inputPassword));
    }
}