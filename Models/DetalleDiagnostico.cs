using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Models
{
    public class DetalleDiagnostico
    {
        public int detalleDiagnostico { get; set; }
        public string descripcion { get; set; }
        public Diagnostico idDiagnostico { get; set; }
        public DetalleFicha idDetalleFicha { get; set; }
    }
}
