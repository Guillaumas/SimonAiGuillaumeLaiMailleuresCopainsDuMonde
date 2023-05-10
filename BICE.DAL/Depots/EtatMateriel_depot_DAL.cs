using BICE.DAL.Depots.Interfaces;

namespace BICE.DAL;

using BICE.BLL; //TODO: Bonne pratique???
using System.Data.SqlClient;

public class EtatMateriel_depot_DAL : Depot_DAL, IEtatMateriel_depot_DAL
{
    // public override EtatMateriel_DAL Update(EtatMateriel_DAL p)
    // {
    //     throw new NotImplementedException();
    // }

    public EtatMateriel_DAL GetById(int id)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"SELECT [id]
                                  ,[etat]
                              FROM [dbo].[etat_materiel]
                              WHERE id=@id";
        Command.Parameters.Add(new SqlParameter("@id", id));
        var reader = Command.ExecuteReader();
        EtatMateriel_DAL em = null;
        if (reader.Read())
        {
            var etat = (string)reader["etat"];
            em = new EtatMateriel_DAL(
                (int)reader["id"],
                (EtatMateriel_BLL.EtatMateriel)Enum.Parse(typeof(EtatMateriel_BLL.EtatMateriel), etat)
            );
        }

        ;
        CloseAndDisposeConnexion();
        return em;
    }

    public EtatMateriel_DAL GetByDenomination(EtatMateriel_BLL.EtatMateriel denomination)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"SELECT [id]
                                  ,[etat]
                              FROM [dbo].[etat_materiel]
                              WHERE etat=@denomination";
        Command.Parameters.Add(new SqlParameter("@denomination", denomination.ToString()));
        var reader = Command.ExecuteReader();

        EtatMateriel_DAL em = null;
        if (reader.Read())
        {
            em = new EtatMateriel_DAL(
                (int)reader["id"],
                denomination
            );
        }

        CloseAndDisposeConnexion();
        return em;
    }

    public EtatMateriel_DAL Insert(EtatMateriel_DAL em)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"INSERT INTO [dbo].[etat_materiel]
                                   ([etat])
                             VALUES
                                   (@denomination);
                             SELECT SCOPE_IDENTITY()";
        Command.Parameters.Add(new SqlParameter("@denomination", em.Denomination));
        em.Id = Convert.ToInt32((decimal)Command.ExecuteScalar());
        CloseAndDisposeConnexion();
        return em;
    }

    // public override IEnumerable<EtatMateriel_DAL> GetAll()
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public override void Delete(EtatMateriel_DAL p)
    // {
    //     throw new NotImplementedException();
    // }
}