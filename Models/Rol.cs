using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Models
{
    public class Rol
    {
        public int idRol { get; set; }
        public string nombreRol {get; set; }
        
        public int idCargo { get; set; }
        public int idRolUsuario { get; set; }
    }
}
