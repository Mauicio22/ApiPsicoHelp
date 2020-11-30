using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string rut { get; set; }
        public string app { get; set; }
        public string apm { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string genero { get; set; }

    }
}
