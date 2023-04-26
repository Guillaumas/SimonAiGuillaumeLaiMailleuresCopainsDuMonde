using System.Data.SqlClient;

namespace BICE.DAL;

public class Vehicule_depot_DAL : Depot_DAL<Vehicule_DAL>
{


    public Vehicule_DAL GetByDenomination(string d)
    {
         InitialiseConnexionAndCommand();
         Command.CommandText = @"SELECT * from [dbo].[Vehicule] where denomination=@denomination";
         Command.Parameters.Add(new SqlParameter("@denomination", d));
         var reader = Command.ExecuteReader();
         Vehicule_DAL vDAL = null;
         if (reader.Read())
         {
             vDAL = new Vehicule_DAL(
                 (int)reader["id"],
                 (string)reader["numeros"],
                 (string)reader["denomination"],
                 (string)reader["immatriculation"],
                 (bool)reader["actif"]);
         }
         CloseAndDisposeConnexion();
         return vDAL;
    }

    public Vehicule_DAL GetByNumeros(string n)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"SELECT * from [dbo].[Vehicule] where numero=@numero";
        Command.Parameters.Add(new SqlParameter("@numero", n));
        var reader = Command.ExecuteReader();
        Vehicule_DAL vDAL = null;
        if (reader.Read())
        {
            vDAL = new Vehicule_DAL(
                (int)reader["id"],
                (string)reader["numeros"],
                (string)reader["denomination"],
                (string)reader["immatriculation"],
                (bool)reader["actif"]);
        }
        CloseAndDisposeConnexion();
        return vDAL;
    }
    
    public override Vehicule_DAL Insert(Vehicule_DAL v)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"INSERT INTO [dbo].[vehicule] ([denomination], [immatriculation], [actif]) VALUES (@denomination, @immatriculation, @actif) SELECT SCOPE_IDENTITY()";
        Command.Parameters.AddWithValue("@denomination", v.Denomination);
        Command.Parameters.AddWithValue("@immatriculation", v.Immatriculation);
        Command.Parameters.AddWithValue("@actif", v.Actif);
        
        v.Id = Convert.ToInt32((decimal)Command.ExecuteScalar()); 
        CloseAndDisposeConnexion();
        return v;
    }

    public override IEnumerable<Vehicule_DAL> GetAll()
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"SELECT * FROM [dbo].[vehicule]";
        
        var reader = Command.ExecuteReader();
        var vehicules = new List<Vehicule_DAL>();
        
        if(!reader.HasRows)
        {
            throw new Exception("Reader is empty");
        }

        while (reader.Read())
        {
            vehicules.Add( new Vehicule_DAL(
                (int)reader["id"],
                (string)reader["numeros"],
                (string)reader["denomination"],
                (string)reader["immatriculation"],
                (bool)reader["actif"]
                ));
        }
        
        CloseAndDisposeConnexion();

        if (vehicules.Count == 0 || vehicules == null)
        {
            throw new Exception("VehiculesDAL is null or empty");
        }

        return vehicules;
    }

    
    
    
    
    //TODO: SC - del this shit
    public override void Delete(Vehicule_DAL v)
    {
        throw new NotImplementedException();
    }
    
    public override Vehicule_DAL Update(Vehicule_DAL v)
    {
        throw new NotImplementedException();
    }

    public override Vehicule_DAL GetById(int id)
    {
        throw new NotImplementedException();
    }
}