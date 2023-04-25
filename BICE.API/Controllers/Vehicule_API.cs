using BICE.DTO;
using BICE.SRV;
using Microsoft.AspNetCore.Mvc;

namespace BICE.API.Controllers;

[ApiController]
[Route("")]
public class Vehicule_API
{
    private Vehicule_SRV service;
    
    public Vehicule_API(Vehicule_SRV service)
    {
        this.service = service;
    }
    
    [HttpGet]
    [Route("vehicules")]
    public List<Vehicule_DTO> GetAll()
    {
        var vehicules = service.GetAll();
        return vehicules;
    }
    
    
    [HttpPost]
    [Route("vehicules")]
    public Vehicule_DTO Add(Vehicule_DTO v)
    {
        return service.Add(v);
    }
}