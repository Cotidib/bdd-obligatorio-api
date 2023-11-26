using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using bdd_obligatorio_api.Models;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;

namespace bdd_obligatorio_api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly MySqlConnection _mySqlConnection;
        private readonly string _connectionString;

        public AuthService(MySqlConnection mySqlConnection, IConfiguration configuration)
        {
            _mySqlConnection = mySqlConnection;
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<bool> GetUser(string username)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        //Parámetros para la consulta
                        command.CommandText = "SELECT * FROM Logins WHERE Logid = @Logid";
                        command.Parameters.AddWithValue("@Logid", username);
                        //Ejecutar la consulta
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            // Verificar si hay filas en el resultado
                            return await reader.ReadAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> RegisterUser(User user)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    if (await UserExists(connection, user.Username))
                    {
                        Console.WriteLine($"El usuario '{user.Username}' ya existe.");
                        throw new Exception(message: "El usuario ya existe");
                    }

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO Logins (Logid, Pwd) VALUES (@Logid, @Pwd)";
                        command.Parameters.AddWithValue("@Logid", user.Username);
                        command.Parameters.AddWithValue("@Pwd", user.Password);
                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        return rowsAffected > 0;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<User?> LoginUser(string Username)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Logins WHERE Logid = @Logid";
                        command.Parameters.AddWithValue("@Logid", Username);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new User(
                                    reader.GetString(reader.GetOrdinal("Logid")),
                                    reader.GetString(reader.GetOrdinal("Pwd"))
                                );
                            }
                            else
                            {
                                return null;
                            }
                        }

                    }
                }
            }
            catch (SecurityTokenException ex)
            {
                Console.WriteLine($"Error de validación del token: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        private async Task<bool> UserExists(MySqlConnection connection, string username)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM Logins WHERE Logid = @Logid";
                command.Parameters.AddWithValue("@Logid", username);

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
        }
    }
}