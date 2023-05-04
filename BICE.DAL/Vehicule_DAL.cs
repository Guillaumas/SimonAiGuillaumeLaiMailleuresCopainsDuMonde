namespace BICE.DAL;

public class Vehicule_DAL
{
    public int Id { get; set; }
    public string Numero { get; set; }
    public string Denomination { get; set; }
    public string Immatriculation { get; set; }
    public bool Actif { get; set; }
    
    public Vehicule_DAL(int id, string numero, string denomination, string immatriculation, bool actif) 
        => (Id, Numero, Denomination, Immatriculation, Actif) = (id, numero,denomination, immatriculation, actif);
    public Vehicule_DAL(string numero, string denomination, string immatriculation, bool actif) 
        => (Numero, Denomination, Immatriculation, Actif) = (numero,denomination, immatriculation, actif);
}