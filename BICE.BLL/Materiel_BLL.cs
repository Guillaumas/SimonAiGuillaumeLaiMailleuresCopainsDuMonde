namespace BICE.BLL;

public class Materiel_BLL
{
    public int Id_Etat_materiel { get; set; }
    public EtatMateriel_BLL.EtatMateriel Etat_materiel { get; set; }
    public int Nombre_utilisation { get; set; }
    public int? Nombre_utilisation_limite { get; set; }
    public DateTime? Date_expiration { get; set; }
    public DateTime? Date_prochain_controle { get; set; }
    
    public Materiel_BLL(DateTime? dateExpiration, DateTime? dateProchainControle)
        => (Date_expiration, Date_prochain_controle) = (dateExpiration, dateProchainControle);
    public Materiel_BLL(int nombreUtilisation, int? nombreUtilisationLimite, DateTime? dateExpiration, DateTime? dateProchainControle)
    :this( dateExpiration, dateProchainControle)
        => (Nombre_utilisation, Nombre_utilisation_limite) = (nombreUtilisation, nombreUtilisationLimite);
    
    private int IncrementUsesNumber()
    {
        return Nombre_utilisation++;
    }
    private EtatMateriel_BLL.EtatMateriel SetEtatMateriel(EtatMateriel_BLL.EtatMateriel em)
    {
        return Etat_materiel = em;
    }

    public bool MaterielCanBeUse()
    {
        if (Date_expiration != null)
            if (Date_expiration < DateTime.Now)
                return false;
        if (Date_prochain_controle != null)
            if (Date_prochain_controle < DateTime.Now)
                return false;
        return true;
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
        if (Date_prochain_controle != null)
            if (Date_prochain_controle < DateTime.Now)
                SetEtatMateriel(EtatMateriel_BLL.EtatMateriel.AController);
    }
    
    public void ReAsigneEtatMateriel()
    {
        if (Date_expiration != null)
            if (Date_expiration < DateTime.Now)
                SetEtatMateriel(EtatMateriel_BLL.EtatMateriel.AJeter);
        if (Date_prochain_controle != null)
            if (Date_prochain_controle < DateTime.Now)
                SetEtatMateriel(EtatMateriel_BLL.EtatMateriel.AController);
    }
}