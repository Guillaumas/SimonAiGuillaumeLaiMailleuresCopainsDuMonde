namespace BICE.DAL;

public class Materiel_DAL
{
    //TODO: SC - Refacto les constructeurs
    public int Id { get; set; }
    
    public string? Denomination { get; set; }
    public string Code_barre { get; set; }
    public int Nombre_utilisations { get; set; }
    public int? Nombre_utilisations_limite { get; set; }
    public DateTime? Date_expiration { get; set; }
    public DateTime? Date_prochain_controle { get; set; }
    public int Id_categorie { get; set; }
    public int Id_etat_materiel { get; set; }
    public int? Id_vehicule { get; set; }
    
    public Materiel_DAL(int id, string? denomination, string code_barre, int nombre_utilisations, int? nombre_utilisations_limite, DateTime? date_expiration, DateTime? date_prochain_controle) 
        => (Id, Denomination, Code_barre, Nombre_utilisations, Nombre_utilisations_limite, Date_expiration, Date_prochain_controle) = (id, denomination, code_barre, nombre_utilisations, nombre_utilisations_limite, date_expiration, date_prochain_controle);
    public Materiel_DAL(int id, string? denomination, string code_barre, int nombre_utilisations, int? nombre_utilisations_limite, DateTime? date_expiration, DateTime? date_prochain_controle, int id_categorie, int id_etat_materiel, int? idVehicule) 
    :this(id, denomination, code_barre, nombre_utilisations, nombre_utilisations_limite, date_expiration, date_prochain_controle)
        => (Id_categorie, Id_etat_materiel, Id_vehicule) = (id_categorie, id_etat_materiel, idVehicule);
    public Materiel_DAL(string? denomination, string code_barre, int nombre_utilisations, int? nombre_utilisations_limite, DateTime? date_expiration, DateTime? date_prochain_controle, int id_categorie, int id_etat_materiel, int? idVehicule)
        => (Denomination, Code_barre, Nombre_utilisations, Nombre_utilisations_limite, Date_expiration, Date_prochain_controle, Id_categorie, Id_etat_materiel, Id_vehicule) = (denomination, code_barre, nombre_utilisations, nombre_utilisations_limite, date_expiration, date_prochain_controle, id_categorie, id_etat_materiel, idVehicule);
}