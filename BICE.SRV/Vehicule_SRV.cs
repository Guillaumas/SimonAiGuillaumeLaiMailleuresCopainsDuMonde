using System.Linq.Expressions;
using BICE.BLL;
using BICE.DAL;
using BICE.DAL.Depots.Interfaces;
using BICE.DTO;
using BICE.SRV.Interfaces_SRV;

namespace BICE.SRV;

public class Vehicule_SRV : IVehicule_SRV
{
    protected IVehicule_depot_DAL depot_vehicule; //TODO: changed Vehicule_depot_DAL to IVehicule_depot_DAL
    protected IHistoriqueInterventionVehicule_depot_DAL depot_historique_intervention_vehicule;

    public Vehicule_SRV()
    {
        this.depot_vehicule = new Vehicule_depot_DAL();
        this.depot_historique_intervention_vehicule = new HistoriqueInterventionVehiculeDepot_DAL();
    }

    public Vehicule_SRV(IVehicule_depot_DAL IvehiculeDepotDal)
        => (depot_vehicule) = (IvehiculeDepotDal);
    public Vehicule_SRV(IVehicule_depot_DAL IvehiculeDepotDal, IHistoriqueInterventionVehicule_depot_DAL interventionVehiculeDepotDal)
        :this(IvehiculeDepotDal)
            => (depot_historique_intervention_vehicule) = (interventionVehiculeDepotDal);
    
    

    public List<Vehicule_DTO> GetAll()
    {
        var vehiculesDAL = depot_vehicule.GetAll();
        var vehiculesDTO = new List<Vehicule_DTO>();

        foreach (var vehiculeDAL in vehiculesDAL)
        {
            vehiculesDTO.Add(CreateDtoByDal(vehiculeDAL));
        }

        if (vehiculesDTO.Count < 0 || vehiculesDTO == null)
        {
            return null;
        }

        return vehiculesDTO;
    }

    //TODO: del this shit
    // public Vehicule_DTO GetByNumeros(string numero)
    // {
    //     var vehiculeDAL = new Vehicule_depot_DAL().GetByNumeros(numero);
    //     return CreateDtoByDal(vehiculeDAL);
    // }

    public Vehicule_DTO Add(Vehicule_DTO dto)
    {
        var vDAL = CreateDalByDto(dto);

        depot_vehicule.Insert(vDAL);

        //TODO: del this shit
        // dto.Denomination = vDAL.Denomination;
        // dto.Immatriculation = vDAL.Immatriculation;
        // dto.Actif = vDAL.Actif;
        // dto.Id = vDAL.Id;

        return dto;
    }

    public Vehicule_DTO Update(Vehicule_DTO dto, string numeroVehicule)
    {
        var vehiculeDal = depot_vehicule.GetByNumeros(numeroVehicule);
        if (vehiculeDal == null)
            return null;
        var newVehiculeDal = CreateDalByDto(dto);
        newVehiculeDal.Id = vehiculeDal.Id;
        dto.Id = vehiculeDal.Id;
        depot_vehicule.Update(newVehiculeDal);
        return dto;
    }

    public Vehicule_DTO DeleteByNumeroVehicule(string numeroVehicule)
    {
        var vehiculeDal = depot_vehicule.GetByNumeros(numeroVehicule);
        var vehiculeDto = CreateDtoByDal(vehiculeDal);

        if (depot_historique_intervention_vehicule.GetAllByVehiculeId(vehiculeDal.Id).Count == 0)
            depot_vehicule.Delete(vehiculeDal);
        else
        {
            vehiculeDto.Actif = false;
            Update(vehiculeDto, vehiculeDto.Numero);
        }

        return vehiculeDto;
    }

    public Vehicule_DAL CreateDalByDto(Vehicule_DTO dto)
    {
        var materielDAL = new Vehicule_DAL(
            dto.Numero,
            dto.Denomination,
            dto.Immatriculation,
            dto.Actif
        );
        return materielDAL;
    }

    public Vehicule_DTO CreateDtoByDal(Vehicule_DAL dal)
    {
        return new Vehicule_DTO()
        {
            Id = dal.Id,
            Denomination = dal.Denomination,
            Immatriculation = dal.Immatriculation,
            Actif = dal.Actif,
            Numero = dal.Numero
        };
    }
}

//TODO: ajout secu: si on entre un immatriculatioin ou numero de vehicule deja exstant
//TODO: si le vehicule est plein, le vider avant de le supprimer