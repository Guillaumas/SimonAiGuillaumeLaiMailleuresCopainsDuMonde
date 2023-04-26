using BICE.BLL;

namespace BICE.DAL;

public class EtatMateriel_DAL
{
    public int Id { get; set; }
    public EtatMateriel_BLL.EtatMateriel Denomination { get; set; }
    public EtatMateriel_DAL(EtatMateriel_BLL.EtatMateriel denomination) 
        => Denomination = denomination;
    public EtatMateriel_DAL(int id, EtatMateriel_BLL.EtatMateriel denomination) 
    :this(denomination)
        => (Id) = (id);
}