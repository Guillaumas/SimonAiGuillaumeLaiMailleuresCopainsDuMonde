namespace BICE.DAL;

public class Vehicule_DAL
{
    public int Id { get; set; }
    public string Denomination { get; set; }
    public string Immatriculation { get; set; }
    public bool Actif { get; set; }
    
    public Vehicule_DAL(int id, string denomination, string immatriculation, bool actif) 
        => (Id, Denomination, Immatriculation, Actif) = (id, denomination, immatriculation, actif);
}