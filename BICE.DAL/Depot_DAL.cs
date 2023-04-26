using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BICE.DAL;

public abstract class Depot_DAL<Type_DAL> : IDepot_DAL<Type_DAL>
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
            //var builder = new ConfigurationBuilder();
            //var config = builder.AddJsonFile("./appsettings.json", false, true).Build();
            // var connectionStringSection = config.GetSection("ConnectionStrings:BICE");
            //if (config == null)
            //{
            //    throw new Exception("Configuration not found.");
            //}
            // ConString = connectionStringSection?.Value ?? throw new Exception($"Connection string not found : {connectionStringSection.Value}");
            
            // TODO : gerer connection string dans appsettings.json 
            ConString = "Data Source=localhost;Initial Catalog=BICE;Persist Security Info=True;User ID=BICE;Password=Guigsetsimsimles+bgdu44";
            //ConString = "Data Source=localhost;Initial Catalog=BICE;Persist Security Info=True;User ID=BICE;Password=Guigsetsimsimles+bgdu44";
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine($"File not found ======> {e.Message}");
            throw;
        }
        //TODO: SC - Verifier que EtatMateriel est rempli
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

    
    
    public abstract IEnumerable<Type_DAL> GetAll();
    public abstract void Delete(Type_DAL p);
    public abstract Type_DAL GetById(int id);
    public abstract Type_DAL Insert(Type_DAL p);
    public abstract Type_DAL Update(Type_DAL p);
}