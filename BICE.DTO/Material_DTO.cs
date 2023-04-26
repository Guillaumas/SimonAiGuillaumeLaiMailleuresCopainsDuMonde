using BICE.DAL; //TODO: Bonne Pratique??

namespace BICE.DTO;

public class Material_DTO : BICE_DTO
{
    public string Code_barre { get; set; }
    public int Nombre_utilisations { get; set; }
    public int? Nombre_utilisations_limite { get; set; }
    public DateTime? Date_expiration { get; set; }
    public DateTime? Date_prochain_controle { get; set; }
    public string? Categorie { get; set; }
    public int? Id_Categorie { get; set; }
    public EtatMateriel Etat_materiel { get; set; }
    public int? Id_Etat_materiel { get; set; }
}