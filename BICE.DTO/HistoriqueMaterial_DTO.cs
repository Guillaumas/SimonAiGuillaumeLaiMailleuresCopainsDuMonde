namespace BICE.DTO;

public class HistoriqueMaterial_DTO
{
    public int? Nb_utilisation { get; set; }
    public int? Nb_utilisation_limite { get; set; }
    public DateTime? Date_expiration { get; set; }
    public DateTime? Date_prochain_control { get; set; }
}