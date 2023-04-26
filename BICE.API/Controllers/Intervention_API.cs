using BICE.DTO;
using BICE.SRV;
using Microsoft.AspNetCore.Mvc;
namespace BICE.API.Controllers;


[ApiController]
[Route("intervention")]
public class Intervention_API
{
    private Intervention_SRV service;
    
    public Intervention_API(Intervention_SRV service)
    {
        this.service = service;
    }
   
    [HttpPost]
    public Intervention_DTO Add(Intervention_DTO i)
    {
        return service.Add(i);
    }
    
    [HttpGet]
    public List<Intervention_DTO> GetAll()
    {
        return service.GetAll();
    }
}



