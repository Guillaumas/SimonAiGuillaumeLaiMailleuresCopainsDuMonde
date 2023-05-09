using BICE.DAL;
using BICE.DTO;
using BICE.BLL;
using BICE.SRV.Interfaces_SRV;

namespace BICE.SRV;
//TODO: create hparent class wit Categorie_SRV and EtatMaterial_SRV
public class EtatMateriel_SRV : IEtatMateriel_SRV
{
    protected EtatMateriel_depot_DAL depot_etatMateriel;
    
    public EtatMateriel_SRV()
    {
        this.depot_etatMateriel = new EtatMateriel_depot_DAL();
    }
    
    public EtatMaterial_DTO GetByDenomination(EtatMateriel_BLL.EtatMateriel denomination)
    {

        var etatMaterielDAL = depot_etatMateriel.GetByDenomination(denomination);

        if (etatMaterielDAL == null)
            return null;
        

        return new EtatMaterial_DTO()
        {
            Id = etatMaterielDAL.Id,
            Denomination = etatMaterielDAL.Denomination
        };
    }
}