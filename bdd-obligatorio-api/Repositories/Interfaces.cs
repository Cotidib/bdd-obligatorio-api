using bdd_obligatorio_api.Models;
namespace bdd_obligatorio_api.Repositories;

public interface IFuncionarioRepository
{
    Funcionario GetFuncionarioByCi(string ci);
    void AddFuncionario(Funcionario funcionario);
    // Otros métodos necesarios
}

public interface IAgendaRepository
{
    void AddAgenda(Agenda agenda);
    // Otros métodos necesarios
}

public interface ICarnetSaludRepository
{
    void AddCarnetSalud(Carnet_Salud carnetSalud);
    // Otros métodos necesarios
}

public interface IPeriodoActualizacionRepository
{
    Periodo_Actualizacion GetPeriodoActual();
    void UpdatePeriodoActual(Periodo_Actualizacion periodo);
    // Otros métodos necesarios
}
