namespace BICE.DAL;

public class Materiel_DAL
{
    public int Id { get; set; }
    
    public string Denomination { get; set; }
    public string Code_barre { get; set; }
    public int Nombre_utilisations { get; set; }
    public int Nombre_utilisations_limite { get; set; }
    public DateTime? Date_expiration { get; set; }
    public DateTime? Date_prochain_controle { get; set; }
    
    public Materiel_DAL(string denomination, string code_barre, int nombre_utilisations, int nombre_utilisations_limite, DateTime? date_expiration, DateTime? date_prochain_controle)
        => (Denomination, Code_barre, Nombre_utilisations, Nombre_utilisations_limite, Date_expiration, Date_prochain_controle) = (denomination, code_barre, nombre_utilisations, nombre_utilisations_limite, date_expiration, date_prochain_controle);
}