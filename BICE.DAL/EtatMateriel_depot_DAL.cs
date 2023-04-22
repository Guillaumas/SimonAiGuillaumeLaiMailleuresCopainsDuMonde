namespace BICE.DAL;
using System.Data.SqlClient;

public class EtatMateriel_depot_DAL : Depot_DAL<EtatMateriel_DAL>
{
    public override EtatMateriel_DAL Update(EtatMateriel_DAL p)
    {
        throw new NotImplementedException();
    }

    public override EtatMateriel_DAL GetById(int id)
    {
        throw new NotImplementedException();
    }
    
    public EtatMateriel_DAL GetByDenomination(string denomination)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"SELECT [id]
                                  ,[etat]
                              FROM [dbo].[etat_materiel]
                              WHERE etat=@denomination";
        Command.Parameters.Add(new SqlParameter("@denomination", denomination));
        var reader = Command.ExecuteReader();
        EtatMateriel_DAL em = null;
        if (reader.Read())
        {
            em = new EtatMateriel_DAL(
                (int)reader["id"],
                (string)reader["etat"]
            );
        }
        CloseAndDisposeConnexion();
        return em;
    }

    public override EtatMateriel_DAL Insert(EtatMateriel_DAL p)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<EtatMateriel_DAL> GetAll()
    {
        throw new NotImplementedException();
    }

    public override void Delete(EtatMateriel_DAL p)
    {
        throw new NotImplementedException();
    }
}