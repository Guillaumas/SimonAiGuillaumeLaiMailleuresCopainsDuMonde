using BICE.DTO;
using BICE.SRV;
namespace BICE.SRV.test;

public class Vehicule_SRV_Test
{
    [Fact]
    public void TestGetAll()
    {
        // Arrange
        var vehicule_srv = new Vehicule_SRV();

        // Act
        var vehicules = vehicule_srv.GetAll();

        // Assert
        Assert.NotNull(vehicules);
        Assert.IsType<List<Vehicule_DTO>>(vehicules);
    }
}

