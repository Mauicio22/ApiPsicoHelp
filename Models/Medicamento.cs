using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Models
{
    public class Medicamento
    {
        public int idMedicamento { get; set; }
        public string nombreMedicamento { get; set; }
        public string cantidadMedicamento { get; set; }
        public string marcaMedicamento { get; set; }
        public string tipoVia { get; set; }
        public RecetaMedica idRecetaMedica { get; set; }
    }
}
