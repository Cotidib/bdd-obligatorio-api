using MySqlConnector;
using bdd_obligatorio_api.Models;

namespace bdd_obligatorio_api.Repositories
{
    public class Periodo_ActualizacionRepository : IPeriodoActualizacionRepository
    {
        private readonly MySqlConnection _connection;

        public Periodo_ActualizacionRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public Periodo_Actualizacion GetPeriodoActual()
        {
            _connection.Open();

            using var command = new MySqlCommand("SELECT * FROM Periodos_Actualizacion ORDER BY Año DESC, Semestre DESC LIMIT 1", _connection);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Periodo_Actualizacion
                {
                    Anio = reader.GetInt32("Anio"),
                    Semestre = reader.GetInt32("Semestre"),
                    // Otros campos
                };
            }

            _connection.Close();

            return null;
        }

        public void UpdatePeriodoActual(Periodo_Actualizacion periodo)
        {
            _connection.Open();

            using var command = new MySqlCommand("INSERT INTO Periodos_Actualizacion (Año, Semestre, Fch_Inicio, Fch_Fin) VALUES (@Año, @Semestre, @Fch_Inicio, @Fch_Fin)", _connection);
            command.Parameters.AddWithValue("@Anio", periodo.Anio);
            command.Parameters.AddWithValue("@Semestre", periodo.Semestre);
            // Otros campos

            command.ExecuteNonQuery();

            _connection.Close();
        }
        // Otros métodos necesarios
    }
}
