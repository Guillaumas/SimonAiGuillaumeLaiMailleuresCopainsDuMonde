namespace BICE.DTO;

public class Intervention_DTO : BICE_DTO
{
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public List<Vehicule_DTO> VehiculeDtos { get; set; }
    public List<string> NumerosVehicule { get; set; }
}