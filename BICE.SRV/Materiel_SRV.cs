namespace BICE.SRV;
using BICE.DTO;
using BICE.DAL;

public class Materiel_SRV : BICE_SRV<Material_DTO>
{
    protected IDepot_DAL<Materiel_DAL> depot_materiel;

    public Materiel_SRV()
    {
        this.depot_materiel = new Materiel_depot_DAL();
    }
    public Material_DTO GetById(int id)
    {
        var materielDAL = depot_materiel.GetById(id);
        return new Material_DTO()
        {
            Id = materielDAL.Id,
            Code_barre = materielDAL.Code_barre,
            Nombre_utilisations = materielDAL.Nombre_utilisations,
            Nombre_utilisations_limite = materielDAL.Nombre_utilisations_limite,
            Date_expiration = materielDAL.Date_expiration,
            Date_prochain_controle = materielDAL.Date_prochain_controle,
            Denomination = materielDAL.Denomination
        };
    }

    public List<Material_DTO> GetAll()
    {
        var materielsDAL = depot_materiel.GetAll();
        var materielsDTO = new List<Material_DTO>();

        foreach (var materielDAL in materielsDAL)
        {
            var materielDTO = new Material_DTO()
            {
                Id = materielDAL.Id,
                Code_barre = materielDAL.Code_barre,
                Nombre_utilisations = materielDAL.Nombre_utilisations,
                Nombre_utilisations_limite = materielDAL.Nombre_utilisations_limite,
                Date_expiration = materielDAL.Date_expiration,
                Date_prochain_controle = materielDAL.Date_prochain_controle,
                Denomination = materielDAL.Denomination
            };
            materielsDTO.Add(materielDTO);
        }

        if (materielsDTO.Count == 0 || materielsDTO == null)
        {
            throw new Exception("MaterielsDTO is null or empty");
        }
        return materielsDTO;
    }

    public Material_DTO Add(Material_DTO dto)
    {
        var materielDAL = new Materiel_DAL(
            dto.Denomination,
            dto.Code_barre,
            dto.Nombre_utilisations,
            dto.Nombre_utilisations_limite,
            dto.Date_expiration,
            dto.Date_prochain_controle);
        depot_materiel.Insert(materielDAL);

        // depot_materiel.Insert(materielDAL);

        dto.Denomination = materielDAL.Denomination;
        dto.Code_barre = materielDAL.Code_barre;
        dto.Nombre_utilisations = materielDAL.Nombre_utilisations;
        dto.Nombre_utilisations_limite = materielDAL.Nombre_utilisations_limite;    
        dto.Date_expiration = materielDAL.Date_expiration;
        dto.Date_prochain_controle = materielDAL.Date_prochain_controle;

        return dto;
    }

    public void Update(Material_DTO dto)
    {
        throw new NotImplementedException();
    }

    public void Delete(Material_DTO dto)
    {
        throw new NotImplementedException();
    }
}