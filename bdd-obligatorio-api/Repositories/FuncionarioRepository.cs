using bdd_obligatorio_api.Models;
using MySqlConnector;
using System;

namespace bdd_obligatorio_api.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly MySqlConnection _connection;

        public FuncionarioRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public Funcionario GetFuncionarioByCi(string ci)
        {
            _connection.Open();

            using var command = new MySqlCommand("SELECT * FROM Funcionarios WHERE Ci = @Ci", _connection);
            command.Parameters.AddWithValue("@Ci", ci);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Funcionario
                {
                    Ci = reader.GetInt32("Ci"),
                    Nombre = reader.GetString("Nombre"),
                    // Otros campos
                };
            }

            _connection.Close();

            return null;
        }

        public void AddFuncionario(Funcionario funcionario)
        {
            _connection.Open();

            using var command = new MySqlCommand("INSERT INTO Funcionarios (Ci, Nombre) VALUES (@Ci, @Nombre)", _connection);
            command.Parameters.AddWithValue("@Ci", funcionario.Ci);
            command.Parameters.AddWithValue("@Nombre", funcionario.Nombre);
            // Otros campos

            command.ExecuteNonQuery();

            _connection.Close();
        }
        // Otros métodos necesarios
    }
}
