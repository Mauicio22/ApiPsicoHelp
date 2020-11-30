using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Models
{
    public class DetalleFicha
    {
        public int detalleFicha { get; set; }
        public string descripcionFicha { get; set; }
        public Usuario idUsuario { get; set; }

        public FichaMedica idFichaMedica { get; set; }
    }
}
