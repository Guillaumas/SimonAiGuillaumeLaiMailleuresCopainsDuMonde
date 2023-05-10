using BICE.DTO;
using BICE.DAL;
using BICE.DAL.Depots.Interfaces;
using BICE.SRV;
using Moq;

namespace BICE.SRV.test;

public class Vehicule_SRV_Test
{
    [Fact]
    public void TestGetAll()
    {
        var mock = new Mock<IVehicule_depot_DAL>();
        mock.Setup(x => x.GetAll()).Returns(new List<Vehicule_DAL>());
        var srv = new Vehicule_SRV(mock.Object);
        var result = srv.GetAll();
        Assert.NotNull(result);
        Assert.IsType<List<Vehicule_DTO>>(result);
        mock.Verify(depot => depot.GetAll(), Times.AtLeastOnce);
    }

    [Fact]
    public void TestAdd()
    {
        var mock = new Mock<IVehicule_depot_DAL>();
        var srv = new Vehicule_SRV(mock.Object);
        var dto = new Vehicule_DTO { Numero = "123", Denomination = "Test", Immatriculation = "ABC-123", Actif = true };
        srv.Add(dto);
        mock.Verify(depot => depot.Insert(It.IsAny<Vehicule_DAL>()), Times.Once);
    }

    [Fact]
    public void TestDeleteByNumeroVehiculeWhenVehiculeNotUsed()
    {
        //TODO: Regrouper avec la fonction en bas??
        var vehiculeDal = new Vehicule_DAL(1, "123", "Test", "ABC123", true);
        var vehiculeDto = new Vehicule_DTO() { Numero = "123", Denomination = "Test", Immatriculation = "ABC123", Actif = true };
        var mockDepotVehicule = new Mock<IVehicule_depot_DAL>();
        var mockDepotHistorique = new Mock<IHistoriqueInterventionVehicule_depot_DAL>();
        
        var depot = new Vehicule_SRV(mockDepotVehicule.Object, mockDepotHistorique.Object);

        mockDepotVehicule.Setup(x => x.GetByNumeros("123")).Returns(vehiculeDal);
        mockDepotHistorique.Setup(x => x.GetAllByVehiculeId(vehiculeDal.Id)).Returns(new List<HistoriqueInterventionVehicule_DAL>());

        var result = depot.DeleteByNumeroVehicule("123");

        mockDepotVehicule.Verify(x => x.Delete(vehiculeDal), Times.Once);
        mockDepotVehicule.Verify(x => x.Update(It.IsAny<Vehicule_DAL>()), Times.Never);
        Assert.Equal(vehiculeDto.Numero, result.Numero);
        Assert.Equal(vehiculeDto.Denomination, result.Denomination);
        Assert.Equal(vehiculeDto.Immatriculation, result.Immatriculation);
        Assert.Equal(vehiculeDto.Actif, result.Actif);
    }

    [Fact]
    public void TestDeleteByNumeroVehiculeWhenVehiculeIsUsed()
    {
        //TODO: Regrouper avec la fonction en haut??
        var vehiculeDal = new Vehicule_DAL(1, "123", "Test", "ABC123", true);
        var vehiculeDto = new Vehicule_DTO() { Numero = "123", Denomination = "Test", Immatriculation = "ABC123", Actif = true };
        var interventionDal = new HistoriqueInterventionVehicule_DAL(1,  DateTime.Now, vehiculeDal.Id,1);
        
        var mockDepotVehicule = new Mock<IVehicule_depot_DAL>();
        var mockDepotHistorique = new Mock<IHistoriqueInterventionVehicule_depot_DAL>();
        
        var depot = new Vehicule_SRV(mockDepotVehicule.Object, mockDepotHistorique.Object);

        mockDepotVehicule.Setup(x => x.GetByNumeros("123")).Returns(vehiculeDal);
        mockDepotHistorique.Setup(x => x.GetAllByVehiculeId(vehiculeDal.Id)).Returns(new List<HistoriqueInterventionVehicule_DAL>(){interventionDal});
        mockDepotVehicule.Setup(x => x.Update(It.IsAny<Vehicule_DAL>())).Verifiable();

        var result = depot.DeleteByNumeroVehicule("123");
        
        Assert.False(result.Actif);
        mockDepotVehicule.Verify(x => x.Update(It.IsAny<Vehicule_DAL>()), Times.Once);
        mockDepotVehicule.Verify(x => x.Delete(vehiculeDal), Times.Never);
    }


    [Fact]
    public void TestUpdate()
    {
        var dto = new Vehicule_DTO { Numero = "123", Denomination = "Test", Immatriculation = "Test123", Actif = true };
        var dal = new Vehicule_DAL(1, "123", "Test", "Test123", true);
        var mock = new Mock<IVehicule_depot_DAL>();
        var srv = new Vehicule_SRV(mock.Object);
        mock.Setup(depot => depot.GetByNumeros(dto.Numero)).Returns(dal);
        srv.Update(dto, "123");
        mock.Verify(depot => depot.Update(It.IsAny<Vehicule_DAL>()), Times.Once);
    }

    [Fact]
    public void TestCreateDtoByDal()
    {
        var srv = new Vehicule_SRV();
        var dal = new Vehicule_DAL(1, "1", "123", "caca", true);
        var dto = srv.CreateDtoByDal(dal);
        Assert.NotNull(dto);
        Assert.Equal(dal.Id, dto.Id);
        Assert.Equal(dal.Numero, dto.Numero);
        Assert.Equal(dal.Denomination, dto.Denomination);
        Assert.Equal(dal.Immatriculation, dto.Immatriculation);
        Assert.Equal(dal.Actif, dto.Actif);
    }

    [Fact]
    public void TestCreateDalByDto()
    {
        var srv = new Vehicule_SRV();
        var dto = new Vehicule_DTO { Numero = "123", Denomination = "Test", Immatriculation = "ABC-123", Actif = true };
        var dal = srv.CreateDalByDto(dto);
        Assert.NotNull(dal);
        Assert.Equal(dto.Numero, dal.Numero);
        Assert.Equal(dto.Denomination, dal.Denomination);
        Assert.Equal(dto.Immatriculation, dal.Immatriculation);
        Assert.Equal(dto.Actif, dal.Actif);
    }
}

