namespace BICE.BLL;

public class Materiel_BLL
{
    //TODO : SC - verifier si le nombre limite d'utilisation n'es pas dépassé??
    public int Categorie { get; set; }
    public int Id_Etat_materiel { get; set; }
    public int Nombre_utilisation { get; set; }
    public int? Nombre_utilisation_limite { get; set; }
    public DateTime? Date_expiration { get; set; }
    
    public Materiel_BLL(int idEtatMateriel, DateTime? dateExpiration)
        => (Id_Etat_materiel, Date_expiration) = (idEtatMateriel, dateExpiration);
    public Materiel_BLL(int nombreUtilisation, int idEtatMateriel, int? nombreUtilisationLimite, DateTime? dateExpiration)
    :this(idEtatMateriel, dateExpiration)
        => (Nombre_utilisation, Nombre_utilisation_limite) = (nombreUtilisation, nombreUtilisationLimite);
    
    private int IncrementUsesNumber()
    {
        return Nombre_utilisation++;
    }
    private int SetIdEtatMateriel(int idEtatMateriel)
    {
        return Id_Etat_materiel = idEtatMateriel;
    }

    public void UpdateOnInterventionReturnUsedMaterial()
    {
        IncrementUsesNumber();
        SetIdEtatMateriel(5);
        if (Nombre_utilisation_limite != null)
            if (Nombre_utilisation >= Nombre_utilisation_limite)
                SetIdEtatMateriel(6);
        if (Date_expiration != null)
            if (Date_expiration < DateTime.Now)
                SetIdEtatMateriel(6);
    }
    
    
    public void UpdateOnInterventionReturnNotUsedMaterial()
    {
        SetIdEtatMateriel(5);
        if (Date_expiration != null)
            if (Date_expiration < DateTime.Now)
                SetIdEtatMateriel(6);
    }
}