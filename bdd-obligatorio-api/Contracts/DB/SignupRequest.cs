namespace bdd_obligatorio_api.Contracts.DB
{
    public record SignupRequest
    (
    string ci,
    string nombre,
    string apellido,
    DateTime fechaNacimiento,
    bool tieneCarneSalud,
    DateTime fechaEmision,
    DateTime fechaVencimiento,
    string domicilio,
    string email,
    string telefono
    );
}