using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using BICE.BLL;

namespace BICE.DAL;

public abstract class Depot_DAL
{
    public string ConString { get; set; }
    protected SqlConnection Connexion { get; set; }
    protected SqlCommand Command { get; set; }
    
    // public Depot_DAL()
    // {
    //     var builder = new ConfigurationBuilder();
    //     var config = builder.AddJsonFile("appsettings.json", false, true).Build();
    //     ConString = config.GetSection("ConnectionStrings:default").Value;
    // }
    
    public Depot_DAL()
    {
        try
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./appsettings.json", optional: false, reloadOnChange: true);
            
            var configuration = builder.Build();

            var connectionString = configuration.GetConnectionString("simsim");
            // var connectionString = configuration.GetConnectionString("guigs");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string not found.");
            }

            ConString = connectionString;
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"File not found ======> {e.Message}");
            throw;
        }
    }
    
    protected void InitialiseConnexionAndCommand()
    {
        Connexion = new SqlConnection(ConString);
        Command = Connexion.CreateCommand();
        Connexion.Open();
    }

    protected void CloseAndDisposeConnexion()
    {
        if (Connexion! != null)
        {
            Connexion.Close();
            Connexion.Dispose();
        }

        if (Command!=null)
        {
            Command.Dispose();
        }       
    }

    protected void CreateEtatMaterielInBdd()
    {
        //TODO: SC - Verifier que EtatMateriel est rempli
        InitialiseConnexionAndCommand();
        Command.CommandText = @"select * from etat_materiel";
        var reader = Command.ExecuteReader();
        var etatMaterielBdd = new List<string>();
        while (reader.Read())
        {
            etatMaterielBdd.Add((string)reader["etat"]);
        }

        foreach (EtatMateriel_BLL.EtatMateriel em in Enum.GetValues(typeof(EtatMateriel_BLL.EtatMateriel)))
        {
            if (!etatMaterielBdd.Contains(em.ToString()))
            {
                var emDal = new EtatMateriel_depot_DAL().Insert(new EtatMateriel_DAL(em));
            }
        }
        CloseAndDisposeConnexion();
    }
}