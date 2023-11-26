using bdd_obligatorio_api.Models;
using bdd_obligatorio_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Text;

namespace bdd_obligatorio_api.Controllers
{
    [ApiController]
    [Route("api/logins")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpPost]
        public IActionResult CreateLogin([FromBody] Login login)
        {
            try
            {
                // Verificar si ya existe un login con el mismo ID
                var existingLogin = _loginRepository.GetLoginById(login.Logid);
                if (existingLogin != null)
                {
                    return Conflict($"Ya existe un login con ID {login.Logid}");
                }

                // Hash de la contraseña antes de guardarla
                login.Pwd = HashPassword(login.Pwd);

                _loginRepository.AddLogin(login);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el login: {ex.Message}");
            }
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Login login)
        {
            try
            {
                var storedLogin = _loginRepository.GetLoginById(login.Logid);

                if (storedLogin != null && VerifyPassword(login.Pwd, storedLogin.Pwd))
                {
                    // Autenticación exitosa
                    return Ok();
                }
                else
                {
                    // Autenticación fallida
                    return Unauthorized("Credenciales inválidas");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al autenticar: {ex.Message}");
            }
        }

        // Función para hashear la contraseña
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        // Función para verificar la contraseña hasheada
        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return hashedPassword.Equals(HashPassword(inputPassword));
        }
    }
}
