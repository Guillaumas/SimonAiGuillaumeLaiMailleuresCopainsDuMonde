using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BICE.DAL;
using BICE.DTO;

namespace BICE.WPF.Tools
{
    public class Converter
    {
        public static Materiel_DAL Materiel_DTO_To_DAL(Material_DTO dto)
        {
            return new Materiel_DAL(dto.Denomination, dto.Code_barre, dto.Nombre_utilisations, dto.Nombre_utilisations_limite, dto.Date_expiration, dto.Date_prochain_controle);
        }
    }
}
