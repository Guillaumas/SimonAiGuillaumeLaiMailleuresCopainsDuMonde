using BICE.BLL;
using BICE.DAL;

namespace BICE.DTO;

public class EtatMaterial_DTO : BICE_DTO
{   
    public EtatMateriel_BLL.EtatMateriel Denomination { get; set; } //TODO: Bonne Pratique?? => Override une porp??
}