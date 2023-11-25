using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public async Task<bool> GetUser(Guid id)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        //Parámetros para la consulta
                        command.CommandText = "SELECT * FROM Users WHERE Id = @Id";
                        command.Parameters.AddWithValue("@Id", id);
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
                        command.CommandText = "INSERT INTO Users (Id, Username, Password) VALUES (@Id, @Username, @Password)";
                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Password", user.Password);
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

        public async Task<User?> LoginUser(string Username, string Password)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT Id, Username, Password FROM Users WHERE Username = @Username AND Password = @Password";
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Password", Password);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new User(
                                    reader.GetGuid(reader.GetOrdinal("Id")),
                                    reader.GetString(reader.GetOrdinal("Username")),
                                    reader.GetString(reader.GetOrdinal("Password"))
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
                command.CommandText = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                command.Parameters.AddWithValue("@Username", username);

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
        }
    }
}