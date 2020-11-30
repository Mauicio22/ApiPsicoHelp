using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Models
{
    public class FichaMedica
    {
        public int idFichaMedica { get; set; }
        public string sede { get; set; }
        public string sistemaSaluda { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
