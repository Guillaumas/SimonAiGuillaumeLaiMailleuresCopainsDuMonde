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
    
    
    
    
    //TODO: SC - del this shit??
    public Vehicule_DTO Update(Vehicule_DTO dto)
    {
        throw new NotImplementedException();
    }

    public void Delete(Vehicule_DTO dto)
    {
        throw new NotImplementedException();
    }
    public Vehicule_DTO GetById(int id)
    {
        throw new NotImplementedException();
    }
}