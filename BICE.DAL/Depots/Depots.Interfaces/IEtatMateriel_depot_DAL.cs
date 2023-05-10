namespace BICE.DAL.Depots.Interfaces;

public interface IEtatMateriel_depot_DAL
{
    public EtatMateriel_DAL GetById(int id);
    public EtatMateriel_DAL GetByDenomination(BICE.BLL.EtatMateriel_BLL.EtatMateriel denomination);
    public EtatMateriel_DAL Insert(EtatMateriel_DAL em);
}