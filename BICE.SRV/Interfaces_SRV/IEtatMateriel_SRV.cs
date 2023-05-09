using BICE.DTO;
using BICE.BLL;
    
namespace BICE.SRV.Interfaces_SRV;

public interface IEtatMateriel_SRV
{
    public EtatMaterial_DTO GetByDenomination(EtatMateriel_BLL.EtatMateriel denomination);
}