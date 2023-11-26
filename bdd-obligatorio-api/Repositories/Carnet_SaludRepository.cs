using MySqlConnector;
using bdd_obligatorio_api.Models;

namespace bdd_obligatorio_api.Repositories
{
    public class Carnet_SaludRepository : ICarnetSaludRepository
    {
        private readonly MySqlConnection _connection;

        public Carnet_SaludRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public void AddCarnetSalud(Carnet_Salud carnetSalud)
        {
            _connection.Open();

            using var command = new MySqlCommand("INSERT INTO Carnet_Salud (Ci, Fch_Emision, Fch_Vencimiento, Comprobante) VALUES (@Ci, @Fch_Emision, @Fch_Vencimiento, @Comprobante)", _connection);
            command.Parameters.AddWithValue("@Ci", carnetSalud.Ci);
            command.Parameters.AddWithValue("@Fch_Emision", carnetSalud.Fch_Emision);
            command.Parameters.AddWithValue("@Fch_Vencimiento", carnetSalud.Fch_Vencimiento);
            command.Parameters.AddWithValue("@Comprobante", carnetSalud.Comprobante);
            // Otros campos

            command.ExecuteNonQuery();

            _connection.Close();
        }
        // Otros métodos necesarios
    }
}
