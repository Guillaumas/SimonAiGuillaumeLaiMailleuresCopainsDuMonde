namespace BICE.BLL;

public class Materiel_BLL
{
    //TODO : SC - verifier si le nombre limite d'utilisation n'es pas dépassé??
    public int Id_Etat_materiel { get; set; }
    public EtatMateriel_BLL.EtatMateriel Etat_materiel { get; set; }
    public int Nombre_utilisation { get; set; }
    public int? Nombre_utilisation_limite { get; set; }
    public DateTime? Date_expiration { get; set; }
    
    public Materiel_BLL(DateTime? dateExpiration)
        => (Date_expiration) = (dateExpiration);
    public Materiel_BLL(int nombreUtilisation, int? nombreUtilisationLimite, DateTime? dateExpiration)
    :this( dateExpiration)
        => (Nombre_utilisation, Nombre_utilisation_limite) = (nombreUtilisation, nombreUtilisationLimite);
    
    private int IncrementUsesNumber()
    {
        return Nombre_utilisation++;
    }
    private EtatMateriel_BLL.EtatMateriel SetEtatMateriel(EtatMateriel_BLL.EtatMateriel em)
    {
        return Etat_materiel = em;
    }
    
    public void UpdateOnInterventionReturnUsedMaterial()
    {
        IncrementUsesNumber();
        SetEtatMateriel(EtatMateriel_BLL.EtatMateriel.Stock);
        if (Nombre_utilisation_limite != null)
            if (Nombre_utilisation >= Nombre_utilisation_limite)
                SetEtatMateriel(EtatMateriel_BLL.EtatMateriel.AJeter);
        if (Date_expiration != null)
            if (Date_expiration < DateTime.Now)
                SetEtatMateriel(EtatMateriel_BLL.EtatMateriel.AJeter);
    }
    
    
    public void UpdateOnInterventionReturnNotUsedMaterial()
    {
        SetEtatMateriel(EtatMateriel_BLL.EtatMateriel.Stock);
        if (Date_expiration != null)
            if (Date_expiration < DateTime.Now)
                SetEtatMateriel(EtatMateriel_BLL.EtatMateriel.AJeter);
    }
}