namespace BICE.DAL;

public class Vehicule_DAL
{
    public int Id { get; set; }
    public string Numeros { get; set; }
    public string Denomination { get; set; }
    public string Immatriculation { get; set; }
    public bool Actif { get; set; }
    
    public Vehicule_DAL(int id, string numeros, string denomination, string immatriculation, bool actif) 
        => (Id, Numeros, Denomination, Immatriculation, Actif) = (id, numeros,denomination, immatriculation, actif);
}