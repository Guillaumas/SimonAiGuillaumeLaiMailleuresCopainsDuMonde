namespace BICE.DAL;

public class Categorie_DAL
{
    public int Id { get; set; }
    public string Denomination { get; set; }
    
    public Categorie_DAL(int id, string denomination) 
        => (Id, Denomination) = (id, denomination);
}