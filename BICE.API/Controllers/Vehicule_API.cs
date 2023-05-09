using BICE.DTO;
using BICE.SRV;
using Microsoft.AspNetCore.Mvc;

namespace BICE.API.Controllers;

[ApiController]
[Route("[controller]")]
public class Vehicule_API
{
    private Vehicule_SRV service;
    
    public Vehicule_API(Vehicule_SRV service)
    {
        this.service = service;
    }
    
    [HttpGet]
    [Route("")]
    public List<Vehicule_DTO> GetAll()
    {
        var vehicules = service.GetAll();
        return vehicules;
    }
    
    [HttpDelete] 
    [Route("{numeroVehicule}")]
    public Vehicule_DTO Delete(string numeroVehicule)
    {
        var vehicules = service.DeleteByNumeroVehicule(numeroVehicule);
        return vehicules;
    }
    [HttpPost]
    [Route("")]
    public Vehicule_DTO Add(Vehicule_DTO v)
    {
        return service.Add(v);
    }
    [HttpPut]
    [Route("{numeroVehicule}")]
    public Vehicule_DTO Update(Vehicule_DTO v, string numeroVehicule)
    {
        return service.Update(v, numeroVehicule);
    }
}