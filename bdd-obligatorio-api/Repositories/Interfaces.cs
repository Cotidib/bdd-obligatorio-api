using bdd_obligatorio_api.Models;
namespace bdd_obligatorio_api.Repositories;

public interface IAgendaRepository
{
    void AddAgenda(Agenda agenda);
}

public interface IPeriodoActualizacionRepository
{
    Periodo_Actualizacion GetPeriodoActual();
    void UpdatePeriodoActual(Periodo_Actualizacion periodo);
}