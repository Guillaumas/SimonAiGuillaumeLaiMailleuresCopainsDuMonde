using BICE.DTO;
using BICE.DAL;

namespace BICE.SRV.Interfaces_SRV;

public interface IVehicule_SRV
{
    public List<Vehicule_DTO> GetAll();
    public Vehicule_DTO Add(Vehicule_DTO dto);
    public Vehicule_DTO Update(Vehicule_DTO dto, string numeroVehicule);
    public Vehicule_DTO DeleteByNumeroVehicule(string numeroVehicule);
    Vehicule_DTO CreateDtoByDal(Vehicule_DAL materielDAL); //TODO herité de qulqueChose?
    Vehicule_DAL CreateDalByDto(Vehicule_DTO materielDAL); //TODO herité de qulqueChose?
}