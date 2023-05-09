using BICE.DAL;
using BICE.DTO;
using BICE.SRV.Interfaces_SRV;

namespace BICE.SRV;

public class Intervention_SRV : IIntervention_SRV
{
    protected Intervention_depot_DAL depot_intervention;
    protected Vehicule_depot_DAL depot_vehicule;
    protected Materiel_depot_DAL depot_materiel;
    protected HistoriqueInterventionVehicule_depot_DAL depot_historiqueInterventionVehicule;
    protected HistoriqueMateriel_depot_DAL depot_historiqueMateriel;
    public Intervention_SRV()
    {
        depot_intervention = new Intervention_depot_DAL();
        depot_historiqueInterventionVehicule = new HistoriqueInterventionVehicule_depot_DAL();
        depot_vehicule = new Vehicule_depot_DAL();
        depot_materiel = new Materiel_depot_DAL();
        depot_historiqueMateriel = new HistoriqueMateriel_depot_DAL();
    }

    public List<Intervention_DTO> GetAll()
    {
        var interventionsDAL = depot_intervention.GetAll();
        var interventionsDTO = new List<Intervention_DTO>();

        foreach (var i in interventionsDAL)
        {
            interventionsDTO.Add(new Intervention_DTO()
            {
                Id = i.Id,
                Date = i.Date,
                Denomination = i.Denomination,
                Description = i.Description
            });
        }

        return interventionsDTO;
    }

    public Intervention_DTO Add(Intervention_DTO dto)
    {
        var interventionDAL = new Intervention_DAL(
            dto.Id,
            dto.Date,
            dto.Denomination,
            dto.Description
        );
        
        //Ajout des vehicules dans l'historique
        foreach (var numero in dto.NumerosVehicule)
        {
            var historiqueInterventionVehiculeDal = depot_historiqueInterventionVehicule.Insert(new HistoriqueInterventionVehicule_DAL(
                dto.Date,
                depot_vehicule.GetByNumeros(numero).Id,
                interventionDAL.Id
            ));

            foreach (var materiel in depot_materiel.GetAllByIdVehicule(depot_vehicule.GetByNumeros(numero).Id))
            {
                var historiqueMaterielDal = new HistoriqueMateriel_DAL(
                    historiqueInterventionVehiculeDal.Id,
                    materiel.Nombre_utilisations,
                    materiel.Nombre_utilisations_limite,
                    materiel.Date_expiration,
                    materiel.Date_prochain_controle
                );
                depot_historiqueMateriel.Insert(historiqueMaterielDal);
            }
        }
        
        //Ajout des materiel à l'historique
        
        depot_intervention.Insert(interventionDAL);
        
        dto.Date = interventionDAL.Date;
        dto.Denomination = interventionDAL.Denomination;
        dto.Description = interventionDAL.Description;
        dto.Id = interventionDAL.Id;
        
        return dto;
    }
}