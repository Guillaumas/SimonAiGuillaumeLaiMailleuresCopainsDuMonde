using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using BICE.DTO;
using Newtonsoft.Json;

namespace BICE.WPF.Tools
{
    internal static class DTO_and_JSON_translator
    {

        public static string ToJson(this BICE_DTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public static List<BICE_DTO> BiceDtoFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<BICE_DTO>>(json);
        }

        public static string ToJson(this Categorie_DTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public static List<Categorie_DTO> CategorieDtoFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<Categorie_DTO>>(json);
        }

        public static string ToJson(this EtatMaterial_DTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public static List<EtatMaterial_DTO> EtatMaterialDtoFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<EtatMaterial_DTO>>(json);
        }

        public static string ToJson(this HistoriqueInterventionVehicule_DTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public static List<HistoriqueInterventionVehicule_DTO> HistoriqueInterventionVehiculeDtoFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<HistoriqueInterventionVehicule_DTO>>(json);
        }

        public static string ToJson(this HistoriqueMaterial_DTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public static List<HistoriqueMaterial_DTO> HistoriqueMaterialDtoFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<HistoriqueMaterial_DTO>>(json);
        }

        public static string ToJson(this Intervention_DTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public static List<Intervention_DTO> InterventionDtoFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<Intervention_DTO>>(json);
        }

        public static string ToJson(this Material_DTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public static List<Material_DTO> MaterialDtoFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<Material_DTO>>(json);
        }

        public static string ToJson(this Vehicule_DTO dto)
        {
            return JsonConvert.SerializeObject(dto);
        }

        public static List<Vehicule_DTO> VehiculeDtoFromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<Vehicule_DTO>>(json);
        }
    }
}