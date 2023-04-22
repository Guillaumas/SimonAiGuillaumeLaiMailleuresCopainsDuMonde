namespace BICE.DAL;
using System.Data.SqlClient;

public class Categorie_depot_DAL : Depot_DAL<Categorie_DAL>
{
    public override Categorie_DAL Insert(Categorie_DAL c)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"INSERT INTO [dbo].[categorie]
                                   ([denomination])
                             VALUES
                                   (@denomination);
                             SELECT SCOPE_IDENTITY()";
        Command.Parameters.Add(new SqlParameter("@denomination", c.Denomination));
        c.Id = Convert.ToInt32((decimal)Command.ExecuteScalar());
        CloseAndDisposeConnexion();
        return c;
    }

    public override Categorie_DAL Update(Categorie_DAL c)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"UPDATE [dbo].[categorie] 
                               SET [denomination] = @denomination
                             WHERE id=@id";
        Command.Parameters.Add(new SqlParameter("@denomination", c.Denomination));
        Command.ExecuteNonQuery();
        
        CloseAndDisposeConnexion();
        return c;
    }

    public override Categorie_DAL GetById(int id)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"SELECT [id]
                                  ,[denomination]
                              FROM [dbo].[categorie]
                              WHERE id=@id";
        Command.Parameters.Add(new SqlParameter("@id", id));
        var reader = Command.ExecuteReader();
        
        Categorie_DAL c = null;
        if (reader.Read())
        {
            c = new Categorie_DAL(
                (string)reader["denomination"]
            );
        }
        CloseAndDisposeConnexion();
        return c;
    }


    public override IEnumerable<Categorie_DAL> GetAll()
    {
        throw new NotImplementedException();
    }

    public override void Delete(Categorie_DAL p)
    {
        throw new NotImplementedException();
    }
}