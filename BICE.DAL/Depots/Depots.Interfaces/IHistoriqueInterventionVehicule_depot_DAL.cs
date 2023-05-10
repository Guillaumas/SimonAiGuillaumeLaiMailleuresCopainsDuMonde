namespace BICE.DAL.Depots.Interfaces;

public interface IHistoriqueInterventionVehicule_depot_DAL
{
    List<HistoriqueInterventionVehicule_DAL> GetAllByVehiculeId(int vehiculeId);
    HistoriqueInterventionVehicule_DAL Insert(HistoriqueInterventionVehicule_DAL hiv);
}