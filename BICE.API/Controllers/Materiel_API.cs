using BICE.DTO;
using BICE.SRV;
using Microsoft.AspNetCore.Mvc;

namespace BICE.API.Controllers;

[ApiController]
[Route("")]
public class Materiel_API
{
    
    private Materiel_SRV service;

    
    public Materiel_API(Materiel_SRV service)
    {
        this.service = service;
    }
    
    [HttpGet]
    public List<Material_DTO> GetAll()
    {
        var materiels = service.GetAll();
        return materiels;
    }

    [HttpPost]
    public Material_DTO Add(Material_DTO materiel)
    {
        return service.Add(materiel);
    }
}