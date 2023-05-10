using BICE.DTO;
using BICE.DAL;
using BICE.DAL.Depots.Interfaces;
using BICE.SRV;
using Moq;

namespace BICE.SRV.test;

public class Materiel_SRV_Test
{
    [Fact]
    public void TestGetAll()
    {
        //TODO: Verfi avec vrai valeur
        var mock = new Mock<IMateriel_depot_DAL>();
        mock.Setup(x => x.GetAll()).Returns(new List<Materiel_DAL>());
        var srv = new Materiel_SRV(mock.Object);
        var result = srv.GetAll();
        Assert.NotNull(result);
        Assert.IsType<List<Material_DTO>>(result);
        mock.Verify(depot => depot.GetAll(), Times.AtLeastOnce);
    }
    
    [Fact]
    public void TestGetAllByEtatMaterielDenomination()
    {
        // Arrange
        var mockEtatMaterielDepot = new Mock<IEtatMateriel_depot_DAL>();
        var mockMaterielDepot = new Mock<IMateriel_depot_DAL>();

        var etatMateriel = new EtatMateriel_DAL { Denomination = "Test Denomination" };
        mockEtatMaterielDepot.Setup(x => x.GetByDenomination(etatMateriel.Denomination)).Returns(etatMateriel);

        var materielDalList = new List<Materiel_DAL>
        {
            new Materiel_DAL { Id = 1, EtatMateriel = etatMateriel },
            new Materiel_DAL { Id = 2, EtatMateriel = etatMateriel }
        };
        mockMaterielDepot.Setup(x => x.GetALLByEtatMateriel(etatMateriel)).Returns(materielDalList);

        var expected = new List<Material_DTO>
        {
            new Material_DTO { Id = 1, EtatMateriel = new EtatMateriel_DTO { Denomination = "Test Denomination" } },
            new Material_DTO { Id = 2, EtatMateriel = new EtatMateriel_DTO { Denomination = "Test Denomination" } }
        };

        var srv = new Materiel_SRV(mockMaterielDepot.Object, mockEtatMaterielDepot.Object);

        // Act
        var result = srv.GetAllByEtatMaterielDenomination(etatMateriel.Denomination);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<Material_DTO>>(result);
        Assert.Equal(expected.Count, result.Count);

        for (int i = 0; i < expected.Count; i++)
        {
            Assert.Equal(expected[i].Id, result[i].Id);
            Assert.Equal(expected[i].EtatMateriel.Denomination, result[i].EtatMateriel.Denomination);
        }

        mockEtatMaterielDepot.Verify(depot => depot.GetByDenomination(etatMateriel.Denomination), Times.Once);
        mockMaterielDepot.Verify(depot => depot.GetALLByEtatMateriel(etatMateriel), Times.Once);
    }

}