namespace BICE.DAL;

public class HistoriqueInterventionVehicule_DAL
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Id_Vehicule { get; set; }
    public int Id_Intervention { get; set; }
    
    public HistoriqueInterventionVehicule_DAL(DateTime date, int id_Vehicule, int id_Intervention)
    => (Date, Id_Vehicule, Id_Intervention) = (date, id_Vehicule, id_Intervention);
    
    public HistoriqueInterventionVehicule_DAL(int id, DateTime date, int id_Vehicule, int id_Intervention)
        :this(date, id_Vehicule, id_Intervention)
            => (Id) = (id);
}