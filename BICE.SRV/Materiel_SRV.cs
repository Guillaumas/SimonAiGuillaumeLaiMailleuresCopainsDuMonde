using System.ComponentModel.Design;
using BICE.BLL;

namespace BICE.SRV;
using BICE.DTO;
using BICE.DAL;

public class Materiel_SRV : BICE_SRV<Material_DTO>
{
    protected Materiel_depot_DAL depot_materiel;
    protected EtatMateriel_depot_DAL depot_EtatMateriel; //TODO: Bonne pratique???
    protected Categorie_depot_DAL depot_categorie; //TODO: Bonne pratique???
    protected Vehicule_depot_DAL depot_vehicule; //TODO: Bonne pratique???
    
    protected Categorie_SRV categorieSRV;
    protected EtatMateriel_SRV etatMaterielSRV;
    

    public Materiel_SRV()
    {
        //TODO : trop de chose dans le constructeur??
        this.depot_materiel = new Materiel_depot_DAL();
        this.depot_EtatMateriel = new EtatMateriel_depot_DAL();
        this.categorieSRV = new Categorie_SRV();
        this.etatMaterielSRV = new EtatMateriel_SRV();
        this.depot_categorie = new Categorie_depot_DAL();
        this.depot_vehicule = new Vehicule_depot_DAL();
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
            var categorie = new Categorie_SRV().GetById(materielDAL.Id_categorie);
            var etat_materiel = new EtatMateriel_SRV().GetById(materielDAL.Id_etat_materiel);
            
            var materielDTO = new Material_DTO()
            {
                Id = materielDAL.Id,
                Code_barre = materielDAL.Code_barre,
                Nombre_utilisations = materielDAL.Nombre_utilisations,
                Nombre_utilisations_limite = materielDAL.Nombre_utilisations_limite,
                Date_expiration = materielDAL.Date_expiration,
                Date_prochain_controle = materielDAL.Date_prochain_controle,
                Denomination = materielDAL.Denomination,
                Id_Categorie = materielDAL.Id_categorie,
                Id_Etat_materiel = materielDAL.Id_etat_materiel,
                Categorie = categorie.Denomination, 
                Etat_materiel = etat_materiel.Denomination
            };
            materielsDTO.Add(materielDTO);
        }

        if (materielsDTO.Count == 0 || materielsDTO == null)
        {
            throw new Exception("MaterielsDTO is null or empty");
        }
        return materielsDTO;
    }
    
    public List<Material_DTO> AddByList(List<Material_DTO> dtos)
    {
        foreach (var dto in dtos)
        {
            dto.Etat_materiel= EtatMateriel.Stock; //TODO: bll??
            Add(dto);
        }

        return dtos;
    }

    public Material_DTO Add(Material_DTO dto)
    {

        var id_categorie = categorieSRV.GetByDenomination(dto.Categorie) == null ? categorieSRV.Add(new Categorie_DTO(){Denomination = dto.Categorie}).Id : categorieSRV.GetByDenomination(dto.Categorie).Id;
        var id_etat_materiel = etatMaterielSRV.GetByDenomination(dto.Etat_materiel) == null ? etatMaterielSRV.Add(new EtatMaterial_DTO(){Denomination = dto.Etat_materiel}).Id : etatMaterielSRV.GetByDenomination(dto.Etat_materiel).Id;

        var materielDAL = new Materiel_DAL(
            dto.Denomination,
            dto.Code_barre,
            dto.Nombre_utilisations,
            dto.Nombre_utilisations_limite,
            dto.Date_expiration,
            dto.Date_prochain_controle, 
            id_categorie,
            id_etat_materiel);
        
        depot_materiel.Insert(materielDAL);

        dto.Denomination = materielDAL.Denomination;
        dto.Code_barre = materielDAL.Code_barre;
        dto.Nombre_utilisations = materielDAL.Nombre_utilisations;
        dto.Nombre_utilisations_limite = materielDAL.Nombre_utilisations_limite;    
        dto.Date_expiration = materielDAL.Date_expiration;
        dto.Date_prochain_controle = materielDAL.Date_prochain_controle;

        return dto;
    }

    public Material_DTO Update(Material_DTO dto)
    {
        throw new NotImplementedException();
    }

    public Material_DTO UpdateByCodeBarre(Material_DTO dto)
    {
        //TODO Supprimer cette fonction de merde si toujours inutilisée
        var materielDAL = depot_materiel.GetByCodeBarre(dto.Code_barre);
        materielDAL = depot_materiel.UpdateByCodeBarre(materielDAL);
        dto.Denomination = materielDAL.Denomination;
        dto.Code_barre = materielDAL.Code_barre;
        dto.Nombre_utilisations = materielDAL.Nombre_utilisations;
        dto.Nombre_utilisations_limite = materielDAL.Nombre_utilisations_limite;
        dto.Date_expiration = materielDAL.Date_expiration;
        dto.Date_prochain_controle = materielDAL.Date_prochain_controle;
        return dto;
    }
    
    public List<Material_DTO> UpdateOnInterventionReturnUsedMaterials(List<Material_DTO> dtos)
    {
        List<Material_DTO> newDtos = new List<Material_DTO>();
        if (dtos != null || dtos.Count == 0)
        {
            foreach (var dto in dtos)
            {
                var materielDAL = depot_materiel.GetByCodeBarre(dto.Code_barre);
                if (materielDAL != null)
                {
                    //TODO:Gérer l'erreur...
                    Materiel_BLL materielBll = new Materiel_BLL(materielDAL.Nombre_utilisations, materielDAL.Id_etat_materiel, materielDAL.Nombre_utilisations_limite, materielDAL.Date_expiration);
                    materielBll.UpdateOnInterventionReturnUsedMaterial();
                    
                    materielDAL.Nombre_utilisations = materielBll.Nombre_utilisation;
                    materielDAL.Id_etat_materiel = materielBll.Id_Etat_materiel;
                    
                    depot_materiel.Update(materielDAL);

                    newDtos.Add(CreateDtoByDal(materielDAL));
                }
            }
            return newDtos;
        }
        return dtos;
    }
    
    public List<Material_DTO> UpdateOnInterventionReturnNotUsedMaterials(List<Material_DTO> dtos)
    {
        List<Material_DTO> newDtos = new List<Material_DTO>();
        if (dtos != null || dtos.Count == 0)
        {
            foreach (var dto in dtos)
            {
                var materielDAL = depot_materiel.GetByCodeBarre(dto.Code_barre);
                if (materielDAL != null)
                {
                    
                    Materiel_BLL materielBll = new Materiel_BLL(materielDAL.Id_etat_materiel, materielDAL.Date_expiration);
                    materielBll.UpdateOnInterventionReturnNotUsedMaterial();
                    materielDAL.Id_etat_materiel = materielBll.Id_Etat_materiel;
                    
                    depot_materiel.Update(materielDAL);
                    
                    newDtos.Add(CreateDtoByDal(materielDAL));
                }
            }
            return newDtos;
        }
    return dtos;
    }

    public void Delete(Material_DTO dto)
    {
        throw new NotImplementedException();
    }
    
    
    
    //TODO: Bonne pratique??
    public Material_DTO CreateDtoByDal(Materiel_DAL materielDAL)
    {
        var dto = new Material_DTO();
        dto.Denomination = materielDAL.Denomination;
        dto.Code_barre = materielDAL.Code_barre;
        dto.Nombre_utilisations = materielDAL.Nombre_utilisations;
        dto.Nombre_utilisations_limite = materielDAL.Nombre_utilisations_limite;
        dto.Date_expiration = materielDAL.Date_expiration;
        dto.Date_prochain_controle = materielDAL.Date_prochain_controle;
        dto.Etat_materiel = depot_EtatMateriel.GetById(materielDAL.Id_etat_materiel).Denomination;
        dto.Categorie = depot_categorie.GetById(materielDAL.Id_categorie).Denomination;
        dto.Id_Categorie = materielDAL.Id_categorie;
        dto.Id_Etat_materiel = materielDAL.Id_etat_materiel;

        return dto;
    }
}

//TODO: unique code barre