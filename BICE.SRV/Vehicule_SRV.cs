using BICE.DAL;
using BICE.DTO;

namespace BICE.SRV;

public class Vehicule_SRV : BICE_SRV<Vehicule_DTO>
{
    protected Vehicule_depot_DAL depot_vehicule;
    
    public Vehicule_SRV()
    {
        this.depot_vehicule = new Vehicule_depot_DAL();
    }
    


    public List<Vehicule_DTO> GetAll()
    {
        var vehiculesDAL = new Vehicule_depot_DAL().GetAll();
        var vehiculesDTO = new List<Vehicule_DTO>();

        foreach (var vehiculeDAL in vehiculesDAL)
        {
            vehiculesDTO.Add(new Vehicule_DTO()
            {
                Id = vehiculeDAL.Id,
                Denomination = vehiculeDAL.Denomination,
                Immatriculation = vehiculeDAL.Immatriculation,
                Actif = vehiculeDAL.Actif,
                Numero = vehiculeDAL.Numero
            });
        }

        if (vehiculesDTO.Count < 0 || vehiculesDTO == null)
        {
            return null;
        }
        return vehiculesDTO;
    }

    public List<Vehicule_DTO> AddByList(List<Vehicule_DTO> dtos)
    {
        var vehiculesDTO = new List<Vehicule_DTO>();
        foreach (var dto in dtos)
        {
            vehiculesDTO.Add(Add(dto));
        }

        return vehiculesDTO;
    }

    public Vehicule_DTO Add(Vehicule_DTO dto)
    {
        var vDAL = new Vehicule_DAL(
            dto.Id,
            dto.Numero,
            dto.Denomination,
            dto.Immatriculation,
            dto.Actif);
        
        depot_vehicule.Insert(vDAL);
        
        dto.Denomination = vDAL.Denomination;
        dto.Immatriculation = vDAL.Immatriculation;
        dto.Actif = vDAL.Actif;
        dto.Id = vDAL.Id;

        return dto;
    }

    public Vehicule_DTO Update(Vehicule_DTO dto)
    {
        throw new NotImplementedException();
    }

    public Vehicule_DTO GetByDenomination(string d)
    {
        var vDAL = depot_vehicule.GetByDenomination(d);
        return new Vehicule_DTO()
        {
            Actif = vDAL.Actif,
            Denomination = vDAL.Denomination,
            Id = vDAL.Id,
            Immatriculation = vDAL.Immatriculation
        };
    }
    
    public Vehicule_DTO Update(Vehicule_DTO dto, string numeroVehicule)
    {
        var vehiculeDal = depot_vehicule.GetByNumeros(numeroVehicule);
        var newVehiculeDal = CreateDalByDto(dto);
        newVehiculeDal.Id = vehiculeDal.Id;
        dto.Id = vehiculeDal.Id;
        depot_vehicule.Update(newVehiculeDal);
        return dto;
    }

    public Vehicule_DTO DeleteByNumeroVehicule(string numeroVehicule)
    {
        var vehiculeDal = depot_vehicule.GetByNumeros(numeroVehicule);
        var vehiculeDto = new Vehicule_DTO()
        {
            Id = vehiculeDal.Id,
            Denomination = vehiculeDal.Denomination,
            Immatriculation = vehiculeDal.Immatriculation,
            Actif = vehiculeDal.Actif,
            Numero = vehiculeDal.Numero
        };
        //TODO: Verifier si le vehicule a été utilisé en regardant les historiques
        // depot_vehicule.Delete(vehiculeDal);
        return vehiculeDto;
    }
    
    public void Delete(Vehicule_DTO dto)
    {
        throw new NotImplementedException();
    }
    
    //TODO: SC - del this shit??
    public Vehicule_DTO GetById(int id)
    {
        throw new NotImplementedException();
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
}

//TODO: enlever les createDtoByDal, chaque dto doit etre personnalisé en fonction de se que l'on veux envoyer
//TODO: ajout secu: si on entre un immatriculatioin ou numero de vehicule deja exstant