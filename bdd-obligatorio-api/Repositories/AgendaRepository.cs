using MySqlConnector;
using bdd_obligatorio_api.Models;

namespace bdd_obligatorio_api.Repositories
{
    public class AgendaRepository : IAgendaRepository
    {
        private readonly MySqlConnection _connection;

        public AgendaRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public void AddAgenda(Agenda agenda)
        {
            _connection.Open();

            using var command = new MySqlCommand("INSERT INTO Agenda (Ci, Fch_Agenda) VALUES (@Ci, @Fch_Agenda)", _connection);
            command.Parameters.AddWithValue("@Ci", agenda.Ci);
            command.Parameters.AddWithValue("@Fch_Agenda", agenda.Fch_Agenda);

            command.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
