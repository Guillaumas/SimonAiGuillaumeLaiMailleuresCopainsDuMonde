using BICE.DAL;
using BICE.DTO;
using BICE.SRV.Interfaces_SRV;

namespace BICE.SRV;

public class Categorie_SRV : ICategorie_SRV
{
    // protected IDepot_DAL<Categorie_DAL> depot_categorie; TODO:use this instead??
    
    protected Categorie_depot_DAL depot_categorie;
    
    public Categorie_SRV()
    {
        this.depot_categorie = new Categorie_depot_DAL();
    }
    
    public Categorie_DTO GetByDenomination(string? denomination)
    {
        var categorieDAL = depot_categorie.GetByDenomination(denomination);

        if (categorieDAL == null) 
            return null;
        
        return new Categorie_DTO()
        {
            Id = categorieDAL.Id,
            Denomination = categorieDAL.Denomination
        };
    }
    
    //TODO: Delete this shit
    // public Categorie_DTO GetById(int id)
    // {
    //     var categorieDAL = depot_categorie.GetById(id);
    //     return new Categorie_DTO()
    //     {
    //         Id = categorieDAL.Id,
    //         Denomination = categorieDAL.Denomination
    //     };
    // }

    // public List<Categorie_DTO> GetAll()
    // {
    //     throw new NotImplementedException();
    // }

    public Categorie_DTO Add(Categorie_DTO dto)
    {
        var categorieDAL = new Categorie_DAL(
            dto.Denomination);
        depot_categorie.Insert(categorieDAL);
        
        dto.Id = categorieDAL.Id;
        dto.Denomination = categorieDAL.Denomination;
        
        return dto; 
    }

    
    //TODO: Delete this shit
    // public Categorie_DTO Update(Categorie_DTO dto)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public void Delete(Categorie_DTO dto)
    // {
    //     throw new NotImplementedException();
    // }
}