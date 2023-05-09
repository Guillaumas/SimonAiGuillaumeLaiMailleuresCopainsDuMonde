using BICE.DTO;

namespace BICE.SRV.Interfaces_SRV;

public interface ICategorie_SRV
{
    public Categorie_DTO GetByDenomination(string? denomination);
    public Categorie_DTO Add(Categorie_DTO dto);
}