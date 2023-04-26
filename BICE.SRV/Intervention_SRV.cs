using BICE.DAL;
using BICE.DTO;

namespace BICE.SRV;

public class Intervention_SRV : BICE_SRV<Intervention_DTO>
{
    protected Intervention_depot_DAL depot_intervention;

    public Intervention_SRV()
    {
        depot_intervention = new Intervention_depot_DAL();
    }
    
    public Intervention_DTO GetById(int id)
    {
        throw new NotImplementedException();
    }

    public List<Intervention_DTO> GetAll()
    {
        var interventionsDAL = depot_intervention.GetAll();
        var interventionsDTO = new List<Intervention_DTO>();

        foreach (var i in interventionsDAL)
        {
            interventionsDTO.Add(new Intervention_DTO()
            {
                Id = i.Id,
                Date = i.Date,
                Denomination = i.Denomination,
                Description = i.Description
            });
        }

        return interventionsDTO;
    }

    public Intervention_DTO Add(Intervention_DTO dto)
    {
        var interventionDAL = new Intervention_DAL(
            dto.Id,
            dto.Date,
            dto.Denomination,
            dto.Description
        );
        
        depot_intervention.Insert(interventionDAL);
        
        dto.Date = interventionDAL.Date;
        dto.Denomination = interventionDAL.Denomination;
        dto.Description = interventionDAL.Description;
        dto.Id = interventionDAL.Id;
        
        return dto;
    }

    public Intervention_DTO Update(Intervention_DTO dto)
    {
        throw new NotImplementedException();
    }

    public void Delete(Intervention_DTO dto)
    {
        throw new NotImplementedException();
    }
}