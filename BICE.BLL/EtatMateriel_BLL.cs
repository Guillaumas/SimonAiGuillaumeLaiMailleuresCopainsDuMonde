namespace BICE.BLL;

public class EtatMateriel_BLL
{
    public enum EtatMateriel
    {
        Stock,
        Jeté,
        AJeter,
        Vehicule,
        Perdu
    }

        public EtatMateriel GetByDenomination(string denomination)
        {
            foreach (EtatMateriel etat in Enum.GetValues(typeof(EtatMateriel)))
            {
                if (etat.ToString().Equals(denomination))
                {
                    return etat;
                }
            }
            throw new ArgumentException("Etat de matériel non trouvé pour la dénomination spécifiée.");
        }
    }
