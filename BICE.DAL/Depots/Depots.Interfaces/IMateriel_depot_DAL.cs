namespace BICE.DAL.Depots.Interfaces;

public interface IMateriel_depot_DAL
{
    public IEnumerable<Materiel_DAL> GetAll();
    public Materiel_DAL Insert(Materiel_DAL m);
    public Materiel_DAL Update(Materiel_DAL m);
    public Materiel_DAL UpdateByCodeBarre(Materiel_DAL m);
    public int GetIdByCodeBarre(string code_barre);
    public Materiel_DAL GetByCodeBarre(string code_barre);
    public List<Materiel_DAL> GetALLByEtatMateriel(EtatMateriel_DAL em);
    public List<Materiel_DAL> GetAllByIdVehicule(int idVehicule);
}
