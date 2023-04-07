namespace BICE.DTO;

public class Material_DTO : BICE_DTO
{
    public string? Bar_code { get; set; }
    public int? Nombre_utilisation { get; set; }
    public int? Nombre_utilisation_limite { get; set; }
    public DateTime? Date_expiration { get; set; }
    public DateTime? Date_prochain_controle { get; set; }
}