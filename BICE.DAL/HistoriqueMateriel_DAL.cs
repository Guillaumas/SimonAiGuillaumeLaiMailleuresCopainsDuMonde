namespace BICE.DAL;

public class HistoriqueMateriel_DAL
{
    public int? Id { get; set; }
    public int Id_Intervention_Vehicule { get; set; }
    public int Nombre_utilisations { get; set; }
    public int? Nombre_utilisations_limite { get; set; }
    public DateTime? Date_expiration { get; set; }
    public DateTime? Date_prochain_controle { get; set; }

    public HistoriqueMateriel_DAL(int idInterventionVehicule, int nombreUtilisations,
        int? nombreUtilisationsLimite, DateTime? dateExpiration, DateTime? dateProchainControl)
        => (Id_Intervention_Vehicule, Nombre_utilisations, Nombre_utilisations_limite, Date_expiration,
            Date_prochain_controle) = (idInterventionVehicule, nombreUtilisations, nombreUtilisationsLimite,
            dateExpiration, dateProchainControl);
}