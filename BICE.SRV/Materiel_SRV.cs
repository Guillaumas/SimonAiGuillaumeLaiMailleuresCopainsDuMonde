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
        return CreateDtoByDal(depot_materiel.GetById(id));
    }

    public List<Material_DTO> GetAll()
    {
        var materielsDAL = depot_materiel.GetAll();
        var materielsDTO = new List<Material_DTO>();
        foreach (var materielDAL in materielsDAL)
        {
            materielsDTO.Add(CreateDtoByDal(materielDAL));
        }

        return materielsDTO;
    }

    public List<Material_DTO> GetAllByEtatMaterielDenomination(EtatMateriel_BLL.EtatMateriel emDenomination)
    {
        var emDal = depot_EtatMateriel.GetByDenomination(emDenomination);
        var materielsDAL = depot_materiel.GetALLByEtatMateriel(emDal);
        var materielsDTO = new List<Material_DTO>();
        foreach (var materielDAL in materielsDAL)
        {
            materielsDTO.Add(CreateDtoByDal(materielDAL));
        }

        return materielsDTO;
    }

    public List<Material_DTO> UpdateByStock(List<Material_DTO> dtos)
    {
        if (dtos != null || dtos.Count == 0)
        {
            foreach (var dto in dtos)
            {
                var materielDal = CreateDalByDto(dto);
                materielDal.Id_categorie = categorieSRV.GetByDenomination(dto.Categorie) == null
                    ? categorieSRV.Add(new Categorie_DTO() { Denomination = dto.Categorie }).Id
                    : categorieSRV.GetByDenomination(dto.Categorie).Id;
                materielDal.Id_etat_materiel =
                    etatMaterielSRV.GetByDenomination(EtatMateriel_BLL.EtatMateriel.Stock).Id;

                if (depot_materiel.GetByCodeBarre(dto.Code_barre) != null)
                    depot_materiel.UpdateByCodeBarre(materielDal);
                else
                    depot_materiel.Insert(materielDal);
            }
        }

        return dtos;
    }

    public List<Material_DTO> UpdateByVehicule(string numeroVehicule, List<Material_DTO> materialDtos)
    {
        var vehiculeDal = depot_vehicule.GetByNumeros(numeroVehicule);

        //TODO: Obligé de tej les ancien materiel???
        var materielsToAddToStock = depot_materiel.GetAllByIdVehicule(vehiculeDal.Id);
        foreach (var mat in materielsToAddToStock)
        {
            mat.Id_etat_materiel = etatMaterielSRV.GetByDenomination(EtatMateriel_BLL.EtatMateriel.Stock).Id;
            depot_materiel.Update(mat);
        }


        if (materialDtos != null || materialDtos.Count == 0)
        {
            foreach (var materialDto in materialDtos)
            {
                var materielDal = CreateDalByDto(materialDto);
                materielDal.Id_vehicule = vehiculeDal.Id;
                materialDto.Id_vehicule = vehiculeDal.Id;
                materielDal.Id_categorie = depot_categorie.GetByDenomination(materialDto.Categorie).Id;
                materielDal.Id_etat_materiel =
                    etatMaterielSRV.GetByDenomination(EtatMateriel_BLL.EtatMateriel.Vehicule).Id;
                depot_materiel.UpdateByCodeBarre(materielDal);
            }
        }

        return materialDtos;
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
                    Materiel_BLL materielBll = new Materiel_BLL(materielDAL.Nombre_utilisations,
                        materielDAL.Nombre_utilisations_limite,
                        materielDAL.Date_expiration);
                    materielBll.UpdateOnInterventionReturnUsedMaterial();

                    materielDAL.Nombre_utilisations = materielBll.Nombre_utilisation;
                    materielDAL.Id_etat_materiel = etatMaterielSRV.GetByDenomination(materielBll.Etat_materiel).Id;

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
                    Materiel_BLL materielBll =
                        new Materiel_BLL(materielDAL.Date_expiration);
                    materielBll.UpdateOnInterventionReturnNotUsedMaterial();
                    materielDAL.Id_etat_materiel = etatMaterielSRV.GetByDenomination(materielBll.Etat_materiel).Id;

                    depot_materiel.Update(materielDAL);

                    newDtos.Add(CreateDtoByDal(materielDAL));
                }
            }

            return newDtos;
        }

        return dtos;
    }

    public List<Material_DTO> UpdateOnInterventionReturnLostMaterialsByNumeroVehicule(string numeroVehicule)
    {
        List<Material_DTO> materielsDTO = new List<Material_DTO>();
        var materielDals = depot_materiel.GetAllByIdVehicule(depot_vehicule.GetByNumeros(numeroVehicule).Id);
        foreach (var dal in materielDals)
        {
            dal.Id_etat_materiel = etatMaterielSRV.GetByDenomination(EtatMateriel_BLL.EtatMateriel.Perdu).Id;
            dal.Id_vehicule = null;
            depot_materiel.Update(dal);
            materielsDTO.Add(CreateDtoByDal(dal));
        }

        return materielsDTO;
    }


    //TODO fare une fonction de verif (verifi que la liste n'est pas vide, retourn null sinon

    //TODO: Bonne pratique??
    public Material_DTO CreateDtoByDal(Materiel_DAL materielDAL)
    {
        // EtatMateriel_BLL etatMaterielBll = new EtatMateriel_BLL();
        var dto = new Material_DTO();
        dto.Id = materielDAL.Id;
        dto.Denomination = materielDAL.Denomination;
        dto.Code_barre = materielDAL.Code_barre;
        dto.Nombre_utilisations = materielDAL.Nombre_utilisations;
        dto.Nombre_utilisations_limite = materielDAL.Nombre_utilisations_limite;
        dto.Date_expiration = materielDAL.Date_expiration;
        dto.Date_prochain_controle = materielDAL.Date_prochain_controle;
        dto.Categorie = depot_categorie.GetById(materielDAL.Id_categorie).Denomination;
        dto.Etat_materiel = depot_EtatMateriel.GetById(materielDAL.Id_etat_materiel).Denomination;
        dto.String_Etat_materiel = depot_EtatMateriel.GetById(materielDAL.Id_etat_materiel).Denomination.ToString();
        dto.Id_Categorie = materielDAL.Id_categorie;
        dto.Id_Etat_materiel = materielDAL.Id_etat_materiel;
        dto.Id_vehicule = materielDAL.Id_vehicule;
        return dto;
    }

    public Materiel_DAL CreateDalByDto(Material_DTO dto)
    {
        var materielDAL = new Materiel_DAL(
            dto.Denomination,
            dto.Code_barre,
            dto.Nombre_utilisations,
            dto.Nombre_utilisations_limite,
            dto.Date_expiration,
            dto.Date_prochain_controle,
            dto.Id_Categorie,
            dto.Id_Etat_materiel,
            dto.Id_vehicule);
        return materielDAL;
    }


    //TODO: Del this shit
    // public List<Material_DTO> AddByList(List<Material_DTO> dtos)
    // {
    //     // Not implemented but if you delete this I cut your throat 
    //     foreach (var dto in dtos)
    //     {
    //         dto.Etat_materiel = EtatMateriel_BLL.EtatMateriel.Stock; //TODO: bll??
    //         Add(dto);
    //     }
    //
    //     return dtos;
    // }

    public Material_DTO Add(Material_DTO dto)
    {
        throw new NotImplementedException();
        //TODO: Del this shit
        // if (depot_materiel.GetByCodeBarre(dto.Code_barre) != null)
        // {
        //     Update(dto);
        //     return dto;
        // }
        // else
        // {
        //     var materielDal = CreateDalByDto(dto);
        //     materielDal.Id_categorie = categorieSRV.GetByDenomination(dto.Categorie) == null
        //         ? categorieSRV.Add(new Categorie_DTO() { Denomination = dto.Categorie }).Id
        //         : categorieSRV.GetByDenomination(dto.Categorie).Id;
        //     materielDal.Id_etat_materiel = etatMaterielSRV.GetByDenomination(EtatMateriel_BLL.EtatMateriel.Stock).Id;
        //     depot_materiel.Insert(materielDal);
        //     return dto;
        // }
    }

    public Material_DTO Update(Material_DTO dto)
    {
        throw new NotImplementedException();
        //TODO: Del this shit
        // var materielDal = CreateDalByDto(dto);
        // materielDal.Id_categorie = categorieSRV.GetByDenomination(dto.Categorie) == null
        //     ? categorieSRV.Add(new Categorie_DTO() { Denomination = dto.Categorie }).Id
        //     : categorieSRV.GetByDenomination(dto.Categorie).Id;
        // materielDal.Id_etat_materiel = etatMaterielSRV.GetByDenomination(EtatMateriel_BLL.EtatMateriel.Stock).Id;
        // depot_materiel.UpdateByCodeBarre(materielDal);
        // return dto;
    }

    public void Delete(Material_DTO dto)
    {
        throw new NotImplementedException();
        //TODO: Del this shit
    }
}