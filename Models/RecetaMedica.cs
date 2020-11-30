using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Models
{
    public class RecetaMedica
    {
        public int idRecetaMedica { get; set; }
        public string descripcion { get; set; }
        public Usuario idUsuario { get; set; }
    }
}
