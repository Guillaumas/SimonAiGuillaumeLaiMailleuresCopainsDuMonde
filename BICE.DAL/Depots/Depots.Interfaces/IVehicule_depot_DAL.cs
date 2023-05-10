namespace BICE.DAL.Depots.Interfaces;

public interface IVehicule_depot_DAL
{
    Vehicule_DAL GetByNumeros(string n);
    Vehicule_DAL Insert(Vehicule_DAL v);
    List<Vehicule_DAL> GetAll();
    Vehicule_DAL Update(Vehicule_DAL v);
    Vehicule_DAL Delete(Vehicule_DAL v);
}