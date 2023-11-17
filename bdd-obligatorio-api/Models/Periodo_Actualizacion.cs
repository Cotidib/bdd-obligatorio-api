namespace bdd_obligatorio_api.Models;
public class Periodo_Actualizacion{
    public int Anio {get; set;}

    public int Semestre {get; set;}
    public DateOnly Fch_Inicio {get; set; }
    public DateOnly Fch_Fin {get; set;}
}