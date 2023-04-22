using BICE.DAL;
using BICE.DTO;

namespace BICE.SRV;

public class Categorie_SRV : BICE_SRV<Categorie_DTO>
{
    // protected IDepot_DAL<Categorie_DAL> depot_categorie; TODO:use this instead
    
    protected Categorie_depot_DAL depot_categorie;
    
    public Categorie_SRV()
    {
        this.depot_categorie = new Categorie_depot_DAL();
    }
    
    public Categorie_DTO GetByDenomination(string denomination)
    {
        var categorieDAL = depot_categorie.GetByDenomination(denomination);

        return new Categorie_DTO()
        {
            Id = categorieDAL.Id,
            Denomination = categorieDAL.Denomination
        };
    }
    public Categorie_DTO GetById(int id)
    {
        throw new NotImplementedException();
    }

    public List<Categorie_DTO> GetAll()
    {
        throw new NotImplementedException();
    }

    public Categorie_DTO Add(Categorie_DTO dto)
    {
        throw new NotImplementedException();
    }

    public void Update(Categorie_DTO dto)
    {
        throw new NotImplementedException();
    }

    public void Delete(Categorie_DTO dto)
    {
        throw new NotImplementedException();
    }
}