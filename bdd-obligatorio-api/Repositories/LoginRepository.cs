using MySqlConnector;
using bdd_obligatorio_api.Models;

namespace bdd_obligatorio_api.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly MySqlConnection _connection;

        public LoginRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public Login GetLoginById(string logId)
        {
            _connection.Open();

            using var command = new MySqlCommand("SELECT * FROM Logins WHERE LogId = @LogId", _connection);
            command.Parameters.AddWithValue("@LogId", logId);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Login
                {
                    Logid = reader.GetString("Logid"),
                    Pwd = reader.GetString("Pwd"),
                    // Otros campos
                };
            }

            _connection.Close();

            return null;
        }

        public void AddLogin(Login login)
        {
            _connection.Open();

            using var command = new MySqlCommand("INSERT INTO Logins (Password) VALUES (@Pwd)", _connection);
            command.Parameters.AddWithValue("@Password", login.Pwd);
            // Otros campos

            command.ExecuteNonQuery();

            _connection.Close();
        }
        // Otros métodos necesarios
    }
}
