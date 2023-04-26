namespace BICE.DAL;

public class EtatMateriel_DAL
{
    public int Id { get; set; }
    public string? Denomination { get; set; }
    public EtatMateriel_DAL(string? denomination) 
        => Denomination = denomination;
    public EtatMateriel_DAL(int id, string? denomination) 
    :this(denomination)
        => (Id) = (id);
}