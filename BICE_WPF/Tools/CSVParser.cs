using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using BICE.DTO;

namespace BICE.WPF.Tools
{
    public class CSVParser
    {
        public static List<Material_DTO> ParseCsvFile(string csvFilePath)
        {
            List<Material_DTO> materiels = new List<Material_DTO>();

            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
            {
                parser.TextFieldType = FieldType.Delimited; // Définit le type de fichier à traiter
                parser.SetDelimiters(";"); // Définit le délimiteur de champ

                while (!parser.EndOfData) // Tant qu'il reste des lignes à lire
                {
                    
                    string[] fields = parser.ReadFields(); // Lit la ligne

                    Material_DTO materiel = new Material_DTO
                    {
                        Code_barre = fields[0],
                        Denomination = fields[1],
                        Categorie = fields[2],
                        Nombre_utilisations = int.Parse(fields[3]),
                        Nombre_utilisations_limite = string.IsNullOrEmpty(fields[4]) ? (int?)null : int.Parse(fields[4]),
                        Date_expiration = string.IsNullOrEmpty(fields[5]) ? (DateTime?)null : DateTime.Parse(fields[5]),
                        Date_prochain_controle = string.IsNullOrEmpty(fields[6]) ? (DateTime?)null : DateTime.Parse(fields[6])
                    }; // Crée un objet Materiel_DTO à partir des données de la ligne

                    materiels.Add(materiel); // Ajoute l'objet à la liste
                }
            }

            return materiels;
        }
    }
}
