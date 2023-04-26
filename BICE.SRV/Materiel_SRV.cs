using System.ComponentModel.Design;

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
            dto.Etat_materiel= "stock"; //TODO: bll??
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
    
    public Material_DTO UpdateOnInterventionReturnUsedMaterial(Material_DTO dto)
    {
        var materielDAL = depot_materiel.GetByCodeBarre(dto.Code_barre);
        if (materielDAL != null)
        {
            //TODO:Gérer l'erreur...
            materielDAL.Nombre_utilisations += 1;
            materielDAL.Id_etat_materiel = depot_EtatMateriel.GetByDenomination("stock").Id; //TODO: SI le stock porte un autre nom, il faut le changer ici

        
            depot_materiel.UpdateByCodeBarre(materielDAL);
        
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
        }
        return dto;
    }
    
    public Material_DTO UpdateOnInterventionReturnNotUsedMaterial(Material_DTO dto)
    {
        //TODO: A faire
        var materielDAL = depot_materiel.GetByCodeBarre(dto.Code_barre);
        if (materielDAL != null)
        {
            //TODO:Gérer l'erreur...
            
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
        }
        return dto;
    }

    public void Delete(Material_DTO dto)
    {
        throw new NotImplementedException();
    }
}

//TODO: faire fonction de creation dto by dal like: "dto.Id_Categorie = materielDAL.Id_categorie; dto.Id_Etat_materiel = materielDAL.Id_etat_materiel; (...)"