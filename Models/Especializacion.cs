using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Models
{
    public class Especializacion
    {
        public int idEspecializacion { get; set; }
        public string tipoEspecializacion { get; set; }
        public Rol idRol { get; set; }
    }
}
