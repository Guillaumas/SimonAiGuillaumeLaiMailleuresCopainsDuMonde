using BICE.DAL;
using BICE.DTO;

namespace BICE.SRV;
//TODO: create hparent class wit Categorie_SRV and EtatMaterial_SRV
public class EtatMateriel_SRV : BICE_SRV<EtatMaterial_DTO>
{
    protected EtatMateriel_depot_DAL depot_etatMateriel;
    
    public EtatMateriel_SRV()
    {
        this.depot_etatMateriel = new EtatMateriel_depot_DAL();
    }
    
    public EtatMaterial_DTO GetByDenomination(EtatMateriel? denomination)
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
    
    public EtatMaterial_DTO GetById(int id)
    {
        var categorieDAL = depot_etatMateriel.GetById(id);
        return new EtatMaterial_DTO()
        {
            Id = categorieDAL.Id,
            Denomination = categorieDAL.Denomination
        };
    }

    public List<EtatMaterial_DTO> GetAll()
    {
        throw new NotImplementedException();
    }

    public EtatMaterial_DTO Add(EtatMaterial_DTO dto)
    {
        var etaMaterielDAL = new EtatMateriel_DAL(
            dto.Denomination);
        depot_etatMateriel.Insert(etaMaterielDAL);
        
        dto.Id = etaMaterielDAL.Id;
        dto.Denomination = etaMaterielDAL.Denomination;
        return dto;
    }

    public EtatMaterial_DTO Update(EtatMaterial_DTO dto)
    {
        throw new NotImplementedException();
    }

    public void Delete(EtatMaterial_DTO dto)
    {
        throw new NotImplementedException();
    }
}