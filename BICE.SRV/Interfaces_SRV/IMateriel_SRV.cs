using BICE.DTO;
using BICE.BLL;
using BICE.DAL;

namespace BICE.SRV.Interfaces_SRV;

public interface IMateriel_SRV
{
    List<Material_DTO> GetAll();
    List<Material_DTO> GetAllByEtatMaterielDenomination(EtatMateriel_BLL.EtatMateriel emDenomination);
    List<Material_DTO> UpdateByStock(List<Material_DTO> dtos);
    List<Material_DTO> UpdateByVehicule(string numeroVehicule, List<Material_DTO> dtos);
    List<Material_DTO> UpdateOnInterventionReturnUsedMaterials(List<Material_DTO> dtos);
    List<Material_DTO> UpdateOnInterventionReturnNotUsedMaterials(List<Material_DTO> dtos);
    List<Material_DTO> UpdateOnInterventionReturnLostMaterialsByNumeroVehicule(string numeroVehicule);
    Material_DTO CreateDtoByDal(Materiel_DAL materielDAL); //TODO herité de qulqueChose?
    Materiel_DAL CreateDalByDto(Material_DTO materielDAL); //TODO herité de qulqueChose?
}