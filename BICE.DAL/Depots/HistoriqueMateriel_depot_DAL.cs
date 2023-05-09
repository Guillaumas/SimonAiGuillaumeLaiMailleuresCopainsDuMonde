namespace BICE.DAL;
using System.Data.SqlClient;

public class HistoriqueMateriel_depot_DAL : Depot_DAL
{
    public HistoriqueMateriel_DAL Insert(HistoriqueMateriel_DAL hm)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"INSERT INTO [BICE].[dbo].[historique_materiel]
				(		 [id_intervention_vehicule]
						,[nb_utilisation]
						,[nb_utilisation_max]
						,[date_expiration]
						,[date_prochain_control]
				)
				VALUES
				(@id_intervention_vehicule, @nb_utilisation, @nb_utilisation_max, @date_expiration, @date_prochain_control))";
        Command.Parameters.AddWithValue("@id_intervention_vehicule", hm.Id_Intervention_Vehicule);
        Command.Parameters.AddWithValue("@nb_utilisation", hm.Nombre_utilisations);
        Command.Parameters.AddWithValue("@nb_utilisation_max", hm.Nombre_utilisations_limite);
        Command.Parameters.AddWithValue("@date_expiration", hm.Date_expiration);
        Command.Parameters.AddWithValue("@date_prochain_control", hm.Date_prochain_controle);
        hm.Id = Convert.ToInt32((decimal)Command.ExecuteScalar());
        CloseAndDisposeConnexion();
        return hm;
    }
}