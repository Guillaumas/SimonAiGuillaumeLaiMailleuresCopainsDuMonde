using BICE.BLL;
using BICE.DAL;
using BICE.DTO;
using BICE.SRV;
using Microsoft.AspNetCore.Mvc;

namespace BICE.API.Controllers;

[ApiController]
[Route("[controller]")]
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
    
    [HttpGet]
    [Route("ToDel")]
    public List<Material_DTO> GetAllByEtatMateriel()
    {
        var materiels = service.GetAllByEtatMaterielDenomination(EtatMateriel_BLL.EtatMateriel.AJeter);
        return materiels;
    }

    [HttpPost]
    public List<Material_DTO> Add(List<Material_DTO> materiel)
    {
        return service.UpdateByStock(materiel);
    }
    [HttpPost]
    [Route("updateVehicule/{numeroVehicule}")]
    public List<Material_DTO> UpdateByVehicule(List<Material_DTO> materiels, string numeroVehicule)
    {
        return service.UpdateByVehicule(numeroVehicule, materiels);
    }
    
    [HttpPost]
    [Route("usedMaterial")]
    public List<Material_DTO> UpdateOnInterventionReturnUsedMaterial(List<Material_DTO> materiels)
    {
        return service.UpdateOnInterventionReturnUsedMaterials(materiels);
    }
    [HttpPost]
    [Route("notusedMaterial")]
    public List<Material_DTO> UpdateOnInterventionReturnNotUsedMaterial(List<Material_DTO> materiels)
    {
        return service.UpdateOnInterventionReturnNotUsedMaterials(materiels);
    }
    [HttpGet]
    [Route("LostMaterial/{numeroVehicule}")]
    public List<Material_DTO> UpdateOnInterventionReturnLostMaterialsByNumeroVehicule(string numeroVehicule)
    {
        return service.UpdateOnInterventionReturnLostMaterialsByNumeroVehicule(numeroVehicule);
    }

}