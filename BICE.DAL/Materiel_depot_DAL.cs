namespace BICE.DAL;
using System.Data.SqlClient;

public class Materiel_depot_DAL : Depot_DAL<Materiel_DAL>
{
    public override Materiel_DAL Insert(Materiel_DAL m)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"INSERT INTO [dbo].[materiel]
                                   ([code_barre], [denomination], [nombre_utilisations], [nombre_utilisations_limite], [date_expiration], [date_prochain_controle], [id_categorie], [id_etat_materiel])
                             VALUES
                                   (@code_barre, @denomination, @nombre_utilisations, @nombre_utilisations_limite, @date_expiration, @date_prochain_controle, @id_categorie, @id_etat_materiel);
                             SELECT SCOPE_IDENTITY()";
        Command.Parameters.Add(new SqlParameter("@denomination", m.Denomination));
        Command.Parameters.Add(new SqlParameter("@code_barre", m.Code_barre));
        Command.Parameters.Add(new SqlParameter("@nombre_utilisations", m.Nombre_utilisations));
        Command.Parameters.Add(new SqlParameter("@nombre_utilisations_limite", m.Nombre_utilisations_limite ?? (object)DBNull.Value));
        Command.Parameters.Add(new SqlParameter("@date_expiration", m.Date_expiration ?? (object)DBNull.Value));
        Command.Parameters.Add(new SqlParameter("@date_prochain_controle", m.Date_prochain_controle ?? (object)DBNull.Value));
        Command.Parameters.Add(new SqlParameter("@id_categorie", m.Id_categorie));
        Command.Parameters.Add(new SqlParameter("@id_etat_materiel", m.Id_etat_materiel));

        m.Id = Convert.ToInt32((decimal)Command.ExecuteScalar());
        CloseAndDisposeConnexion();
        return m;
    }

    public override Materiel_DAL Update(Materiel_DAL m)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"UPDATE [dbo].[materiel] 
                               SET [code_barre] = @code_barre, [denomination] = @denomination, [nombre_utilisationss] = @nombre_utilisations, 
                                   [nombre_utilisationss_limite] = @nombre_utilisations_limite, [date_expiration] = @date_expiration, 
                                   [date_prochain_controle] = @date_prochain_controle
                             WHERE id=@id";
        Command.Parameters.Add(new SqlParameter("@code_barre", m.Code_barre));
        Command.Parameters.Add(new SqlParameter("@nombre_utilisations", m.Nombre_utilisations));
        Command.Parameters.Add(new SqlParameter("@nombre_utilisations_limite", m.Nombre_utilisations_limite));
        Command.Parameters.Add(new SqlParameter("@date_expiration", m.Date_expiration));
        Command.Parameters.Add(new SqlParameter("@date_prochain_controle", m.Date_prochain_controle));
        Command.ExecuteNonQuery();
        
        CloseAndDisposeConnexion();
        return m;
    }

    public override void Delete(Materiel_DAL p)
    {
        throw new NotImplementedException();
        InitialiseConnexionAndCommand();
        Command.CommandText = @"DELETE FROM [dbo].[materiels] 
                             WHERE id=@id";
        Command.Parameters.Add(new SqlParameter("@id", p.Id));
        Command.ExecuteNonQuery();
        CloseAndDisposeConnexion();
    }
    
    
    public override IEnumerable<Materiel_DAL> GetAll()
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"SELECT [id], [code_barre], [denomination], [nombre_utilisations], [nombre_utilisations_limite], [date_expiration], [date_prochain_controle], [id_categorie], [id_etat_materiel]
                             FROM [dbo].[materiel]";
        
        var reader = Command.ExecuteReader();
        
        var materiels = new List<Materiel_DAL>();
        
        if (!reader.HasRows)
        {
            throw new Exception("Reader is empty");
        }

        
        while (reader.Read())
        {
            materiels.Add(new Materiel_DAL(
                (string)reader["denomination"],
                (string)reader["code_barre"],
                (int)reader["nombre_utilisations"],
                

                reader["nombre_utilisations_limite"] != DBNull.Value ? (int)reader["nombre_utilisations_limite"] : null,
                reader["date_expiration"] != DBNull.Value ? (DateTime)reader["date_expiration"] : null,
                reader["date_prochain_controle"] != DBNull.Value ? (DateTime)reader["date_prochain_controle"] : null,
                (int)reader["id_categorie"],
                (int)reader["id_etat_materiel"]
                ));
        }
        CloseAndDisposeConnexion();
        
        if (materiels.Count == 0 || materiels == null)
        {
            throw new Exception("MaterielsDAL is null or empty");
        }
        return materiels;
    }

    public override Materiel_DAL GetById(int id)
    {
        // throw new NotImplementedException();
        
        InitialiseConnexionAndCommand();
        Command.CommandText = @"SELECT [id], [code_barre], [denomination], [nombre_utilisations], [nombre_utilisations_limite], [date_expiration], [date_prochain_controle]
                             FROM [dbo].[materiel]
                             WHERE id=@id";
        Command.Parameters.Add(new SqlParameter("@id", id));
        var reader = Command.ExecuteReader();
        
        Materiel_DAL m = null;
        
        if (reader.Read())
        {
            m = new Materiel_DAL(
                (string)reader["denomination"],
                (string)reader["code_barre"],
                (int)reader["nombre_utilisation"],
                (int)reader["nombre_utilisation_limite"],
                (DateTime)reader["date_expiration"],
                (DateTime)reader["date_prochain_controle"],
                (int)reader["id_categorie"],
                (int)reader["id_etat_materiel"]
                );
        }
        CloseAndDisposeConnexion();
        return m;
    }
}