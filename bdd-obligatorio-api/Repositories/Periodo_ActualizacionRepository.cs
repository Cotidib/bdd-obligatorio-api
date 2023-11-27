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

            using var command = new MySqlCommand("SELECT * FROM Periodos_Actualizacion ORDER BY Anio DESC, Semestre DESC LIMIT 1", _connection);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Periodo_Actualizacion
                {
                    Anio = reader.GetInt32("Anio"),
                    Semestre = reader.GetInt32("Semestre"),
                    Fch_Inicio = reader.GetDateTime("Fch_Inicio"),
                    Fch_Fin = reader.GetDateTime("Fch_Fin")
                };
            }

            _connection.Close();

            return null;
        }

        public void UpdatePeriodoActual(Periodo_Actualizacion periodo)
        {
            _connection.Open();

            using var command = new MySqlCommand("UPDATE Periodos_Actualizacion SET Fch_Inicio = @Fch_Inicio, Fch_Fin = @Fch_Fin WHERE Anio = @Anio AND Semestre = @Semestre", _connection);
            command.Parameters.AddWithValue("@Anio", periodo.Anio);
            command.Parameters.AddWithValue("@Semestre", periodo.Semestre);
            command.Parameters.AddWithValue("@Fch_Inicio", periodo.Fch_Inicio);
            command.Parameters.AddWithValue("@Fch_Fin", periodo.Fch_Fin);

            command.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
