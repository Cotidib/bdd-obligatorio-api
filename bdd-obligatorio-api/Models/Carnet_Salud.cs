namespace bdd_obligatorio_api.Models;
public class Carnet_Salud{
    public int Ci {get; set;}
    public DateOnly Fch_Emision {get; set; }
    public DateOnly Fch_Vencimiento {get; set;}
    public int Comprobante {get; set; }

}