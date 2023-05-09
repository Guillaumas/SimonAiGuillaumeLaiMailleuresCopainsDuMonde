using BICE.DTO;
namespace BICE.SRV.Interfaces_SRV;

public interface IIntervention_SRV
{
    public List<Intervention_DTO> GetAll();
    public Intervention_DTO Add(Intervention_DTO dto);
}