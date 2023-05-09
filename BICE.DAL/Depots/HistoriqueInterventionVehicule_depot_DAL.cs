namespace BICE.DAL;
using System.Data.SqlClient;

public class HistoriqueInterventionVehicule_depot_DAL : Depot_DAL
{
    public List<HistoriqueInterventionVehicule_DAL> GetAllByVehiculeId(int vehiculeId)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"select * from [dbo].[historique_intervention_vehicule] where id_vehicule = @id_vehicule";
        Command.Parameters.Add(new SqlParameter("@id_vehicule", vehiculeId));
        var reader = Command.ExecuteReader();
        var historiqueInterventionVehiculeDALs = new List<HistoriqueInterventionVehicule_DAL>();
        if (reader.Read())
        {
            historiqueInterventionVehiculeDALs.Add(new HistoriqueInterventionVehicule_DAL(
                (int)reader["Id"],
                (DateTime)reader["Date"],
                (int)reader["IdVehicule"],
                (int)reader["IdIntervention"]
                ));
        }
        CloseAndDisposeConnexion();
        return historiqueInterventionVehiculeDALs;
    }
    
    public HistoriqueInterventionVehicule_DAL Insert(HistoriqueInterventionVehicule_DAL hiv)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"INSERT INTO [dbo].[historique_intervention_vehicule]
           ([IdVehicule]
           ,[IdIntervention]
           ,[Date]) VALUES (@IdVehicule, @IdIntervention, @Date) select SCOPE_IDENTITY()";
        Command.Parameters.Add(new SqlParameter("@IdVehicule", hiv.Id_Vehicule));
        Command.Parameters.Add(new SqlParameter("@IdIntervention", hiv.Id_Intervention));
        Command.Parameters.Add(new SqlParameter("@Date", hiv.Date));
        hiv.Id = Convert.ToInt32((decimal)Command.ExecuteScalar());
        CloseAndDisposeConnexion();
        return hiv;
    }
}