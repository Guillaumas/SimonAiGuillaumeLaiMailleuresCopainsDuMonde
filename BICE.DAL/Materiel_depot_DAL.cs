using BICE.BLL;

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
        Command.Parameters.Add(new SqlParameter("@nombre_utilisations_limite",
            m.Nombre_utilisations_limite ?? (object)DBNull.Value));
        Command.Parameters.Add(new SqlParameter("@date_expiration", m.Date_expiration ?? (object)DBNull.Value));
        Command.Parameters.Add(new SqlParameter("@date_prochain_controle",
            m.Date_prochain_controle ?? (object)DBNull.Value));
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
                               SET [denomination] = @denomination, [nombre_utilisations] = @nombre_utilisations, 
                                   [nombre_utilisations_limite] = @nombre_utilisations_limite, [date_expiration] = @date_expiration, 
                                   [date_prochain_controle] = @date_prochain_controle, [id_categorie] = @id_categorie, [id_etat_materiel] = @id_etat_materiel, [id_vehicule] = @id_vehicule
                             WHERE [id]=@id";
        Command.Parameters.Add(new SqlParameter("@id", m.Id));
        Command.Parameters.Add(new SqlParameter("@code_barre", m.Code_barre));
        Command.Parameters.Add(new SqlParameter("@nombre_utilisations", m.Nombre_utilisations));
        Command.Parameters.Add(new SqlParameter("@nombre_utilisations_limite",
            m.Nombre_utilisations_limite ?? (object)DBNull.Value));
        Command.Parameters.Add(new SqlParameter("@date_expiration", m.Date_expiration ?? (object)DBNull.Value));
        Command.Parameters.Add(new SqlParameter("@date_prochain_controle",
            m.Date_prochain_controle ?? (object)DBNull.Value));
        Command.Parameters.Add(new SqlParameter("@id_categorie", m.Id_categorie));
        Command.Parameters.Add(new SqlParameter("@id_etat_materiel", m.Id_etat_materiel));
        Command.Parameters.Add(new SqlParameter("@denomination", m.Denomination));
        Command.Parameters.Add(new SqlParameter("@id_vehicule", m.Id_vehicule ?? (object)DBNull.Value));
        Command.ExecuteReader();
        CloseAndDisposeConnexion();
        return m;
    }

    public Materiel_DAL UpdateByCodeBarre(Materiel_DAL m)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"UPDATE [dbo].[materiel] 
                               SET [denomination] = @denomination, [nombre_utilisations] = @nombre_utilisations, 
                                   [nombre_utilisations_limite] = @nombre_utilisations_limite, [date_expiration] = @date_expiration, 
                                   [date_prochain_controle] = @date_prochain_controle, [id_categorie] = @id_categorie, [id_etat_materiel] = @id_etat_materiel
                             WHERE [code_barre]=@code_barre SELECT SCOPE_IDENTITY()";
        Command.Parameters.Add(new SqlParameter("@code_barre", m.Code_barre));
        Command.Parameters.Add(new SqlParameter("@nombre_utilisations", m.Nombre_utilisations));
        Command.Parameters.Add(new SqlParameter("@nombre_utilisations_limite",
            m.Nombre_utilisations_limite ?? (object)DBNull.Value));
        Command.Parameters.Add(new SqlParameter("@date_expiration", m.Date_expiration ?? (object)DBNull.Value));
        Command.Parameters.Add(new SqlParameter("@date_prochain_controle",
            m.Date_prochain_controle ?? (object)DBNull.Value));
        Command.Parameters.Add(new SqlParameter("@id_categorie", m.Id_categorie));
        Command.Parameters.Add(new SqlParameter("@id_etat_materiel", m.Id_etat_materiel));
        Command.Parameters.Add(new SqlParameter("@denomination", m.Denomination));
        Command.ExecuteNonQuery();

        m.Id = GetIdByCodeBarre(m.Code_barre);
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
        Command.CommandText =
            @"SELECT * FROM [dbo].[materiel]";

        var reader = Command.ExecuteReader();

        var materiels = new List<Materiel_DAL>();

        if (!reader.HasRows) // check si la base de données est vide
        {
            throw new Exception("Reader is empty"); //TODO: create custom exception si la base de données est vide
        }


        while (reader.Read())
        {
            materiels.Add(new Materiel_DAL(
                (int)reader["id"],
                (string)reader["denomination"],
                (string)reader["code_barre"],
                (int)reader["nombre_utilisations"],
                reader["nombre_utilisations_limite"] != DBNull.Value ? (int)reader["nombre_utilisations_limite"] : null,
                reader["date_expiration"] != DBNull.Value ? (DateTime)reader["date_expiration"] : null,
                reader["date_prochain_controle"] != DBNull.Value ? (DateTime)reader["date_prochain_controle"] : null,
                (int)reader["id_categorie"],
                (int)reader["id_etat_materiel"],
                reader["id_vehicule"] != DBNull.Value ? (int)reader["id_vehicule"] : null
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
        Command.CommandText =
            @"SELECT [id], [code_barre], [denomination], [nombre_utilisations], [nombre_utilisations_limite], [date_expiration], [date_prochain_controle]
                             FROM [dbo].[materiel]
                             WHERE id=@id";
        Command.Parameters.Add(new SqlParameter("@id", id));
        var reader = Command.ExecuteReader();

        Materiel_DAL m = null;

        if (reader.Read())
        {
            m = new Materiel_DAL(
                (int)reader["id"],
                (string)reader["denomination"],
                (string)reader["code_barre"],
                (int)reader["nombre_utilisation"],
                (int)reader["nombre_utilisation_limite"],
                (DateTime)reader["date_expiration"],
                (DateTime)reader["date_prochain_controle"],
                (int)reader["id_categorie"],
                (int)reader["id_etat_materiel"],
                reader["id_vehicule"] != DBNull.Value ? (int)reader["id_vehicule"] : null
            );
        }

        CloseAndDisposeConnexion();
        return m;
    }

    public int GetIdByCodeBarre(string code_barre)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"SELECT [id]
                             FROM [dbo].[materiel]
                             WHERE code_barre=@code_barre";
        Command.Parameters.Add(new SqlParameter("@code_barre", code_barre));
        var reader = Command.ExecuteReader();

        int id = 0;

        if (reader.Read())
        {
            id = (int)reader["id"];
        }

        CloseAndDisposeConnexion();
        return id;
    }

    public Materiel_DAL GetByCodeBarre(string code_barre)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText =
            @"SELECT [id], [code_barre], [denomination], [nombre_utilisations], [nombre_utilisations_limite], [date_expiration], [date_prochain_controle], [id_categorie], [id_etat_materiel]
                             FROM [dbo].[materiel]
                             WHERE code_barre=@code_barre";
        Command.Parameters.Add(new SqlParameter("@code_barre", code_barre));
        var reader = Command.ExecuteReader();

        Materiel_DAL m = null;

        if (reader.Read())
        {
            m = new Materiel_DAL(
                (int)reader["id"],
                (string)reader["denomination"],
                (string)reader["code_barre"],
                (int)reader["nombre_utilisations"],
                reader["nombre_utilisations_limite"] != DBNull.Value ? (int)reader["nombre_utilisations_limite"] : null,
                reader["date_expiration"] != DBNull.Value ? (DateTime)reader["date_expiration"] : null,
                reader["date_prochain_controle"] != DBNull.Value ? (DateTime)reader["date_prochain_controle"] : null,
                (int)reader["id_categorie"],
                (int)reader["id_etat_materiel"],
                reader["id_vehicule"] != DBNull.Value ? (int)reader["id_vehicule"] : null
            );
        }

        CloseAndDisposeConnexion();
        return m;
    }

    public List<Materiel_DAL> GetALLByEtatMateriel(EtatMateriel_DAL em)
    {
        //TODO: recupere tout les objet à jeter dans le WPF
        InitialiseConnexionAndCommand();
        Command.CommandText =
            @"SELECT [id], [code_barre], [denomination], [nombre_utilisations], [nombre_utilisations_limite], [date_expiration], [date_prochain_controle], [id_categorie], [id_etat_materiel]
                             FROM [dbo].[materiel]
                             WHERE id_etat_materiel=@id_etat_materiel";
        Command.Parameters.Add(new SqlParameter("@id_etat_materiel", em.Id));
        var reader = Command.ExecuteReader();

        var materiels = new List<Materiel_DAL>();

        while (reader.Read())
        {
            materiels.Add(new Materiel_DAL(
                (int)reader["id"],
                (string)reader["denomination"],
                (string)reader["code_barre"],
                (int)reader["nombre_utilisations"],
                reader["nombre_utilisations_limite"] != DBNull.Value ? (int)reader["nombre_utilisations_limite"] : null,
                reader["date_expiration"] != DBNull.Value ? (DateTime)reader["date_expiration"] : null,
                reader["date_prochain_controle"] != DBNull.Value ? (DateTime)reader["date_prochain_controle"] : null,
                (int)reader["id_categorie"],
                (int)reader["id_etat_materiel"],
                reader["id_vehicule"] != DBNull.Value ? (int)reader["id_vehicule"] : null
            ));
        }

        CloseAndDisposeConnexion();

        if (materiels.Count == 0 || materiels == null)
        {
            throw new Exception("MaterielsDAL is null or empty");
        }

        return materiels;
    }

    public List<Materiel_DAL> GetAllByIdVehicule(int idVehicule)
    {
        InitialiseConnexionAndCommand();
        Command.CommandText = @"SELECT * from materiel where id_vehicule = @idVehicule";
        Command.Parameters.Add(new SqlParameter("@idVehicule", idVehicule));
        var reader = Command.ExecuteReader();
        var materiels = new List<Materiel_DAL>();


        while (reader.Read())
        {
            materiels.Add(new Materiel_DAL(
                (int)reader["id"],
                (string)reader["denomination"],
                (string)reader["code_barre"],
                (int)reader["nombre_utilisations"],
                reader["nombre_utilisations_limite"] != DBNull.Value ? (int)reader["nombre_utilisations_limite"] : null,
                reader["date_expiration"] != DBNull.Value ? (DateTime)reader["date_expiration"] : null,
                reader["date_prochain_controle"] != DBNull.Value ? (DateTime)reader["date_prochain_controle"] : null,
                (int)reader["id_categorie"],
                (int)reader["id_etat_materiel"],
                reader["id_vehicule"] != DBNull.Value ? (int)reader["id_vehicule"] : null
            ));
        }

        CloseAndDisposeConnexion();

        if (materiels.Count == 0 || materiels == null)
        {
            throw new Exception("MaterielsDAL is null or empty");
        }

        return materiels;
    }
}