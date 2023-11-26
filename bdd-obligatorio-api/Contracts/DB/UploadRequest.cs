
namespace bdd_obligatorio_api.Contracts.DB
{
    public record UploadRequest(
      string ci,
      string nombre,
    string apellido,
      DateTime fechaNacimiento,
      bool tieneCarneSalud,
      DateTime fechaEmision,
      DateTime fechaVencimiento
);
}