namespace BICE.DAL;
using System.Data.SqlClient;

public class Intervention_depot_DAL : Depot_DAL<Intervention_DAL>
{
    public override Intervention_DAL Update(Intervention_DAL i)
    {
        throw new NotImplementedException();
    }

    public override Intervention_DAL GetById(int id)
    {
        throw new NotImplementedException();
    }

    public override Intervention_DAL Insert(Intervention_DAL i)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"INSERT INTO [dbo].[intervention]
                               ([date], [denomination], [description])
                               VALUES
                               (@date, @denomination, @description)
                               Select SCOPE_IDENTITY()";
        Command.Parameters.Add(new SqlParameter("@date", i.Date));
        Command.Parameters.Add(new SqlParameter("@denomination", i.Denomination));
        Command.Parameters.Add(new SqlParameter("@description", i.Description));
        i.Id = Convert.ToInt32((decimal)Command.ExecuteScalar());
        CloseAndDisposeConnexion();
        return i;
    }

    public override IEnumerable<Intervention_DAL> GetAll()
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"SELECT [id], [date], [denomination], [description]
                             FROM [dbo].[intervention]";
        
        var reader = Command.ExecuteReader();
        var interventions = new List<Intervention_DAL>();
        if (!reader.HasRows)
        {
            throw new Exception("Reader is empty");
        }

        while (reader.Read())
        {
            interventions.Add(new Intervention_DAL(
                (int)reader["id"],
                (DateTime)reader["date"],
                (string)reader["denomination"],
                (string)reader["description"]
                ));
        }
        CloseAndDisposeConnexion();
        if (interventions.Count == 0 || interventions == null)
        {
            throw new Exception("InterventionsDAL is null or empty");
        }

        return interventions;
    }

    public override void Delete(Intervention_DAL i)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"DELETE FROM [dbo].[intervention]
                                WHERE [id] = @id";
        Command.Parameters.Add(new SqlParameter("@id", i.Id));
        Command.ExecuteNonQuery();
        CloseAndDisposeConnexion();
    }
}