namespace bdd_obligatorio_api.Models;
public class Funcionario{

    public int Ci {get; set; }
    public string Nombre {get; set;}

    public string Apellido {get; set; }

    public DateOnly Fch_Nacimiento {get;set;}
    
    public string Direccion {get; set; }
    public int Telefono {get; set; }
    public string Email {get; set;}
    public string LogId {get; set; }

    public Funcionario(){}
}