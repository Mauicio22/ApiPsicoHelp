using ApiPsicoHelp.Azure;
using ApiPsicoHelp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        [HttpGet("all")]
        public JsonResult ObtenerRoles()
        {
            //json
            var rolesRetornados = RolAzure.obtenerRoles();
            return new JsonResult(rolesRetornados);
        }

        [HttpGet("{Rol}")]
        public JsonResult ObtenerRol(string Rol)
        {
            var conversionExitosa = int.TryParse(Rol, out int idConvertido);

            Models.Usuario rolRetornado;

            if (conversionExitosa)
            {
                rolRetornado = UsuarioAzure.ObtenerUsuario(idConvertido);
            }
            else
            {
                rolRetornado = UsuarioAzure.ObtenerUsuario(Rol);
            }
            if (rolRetornado is null)
            {
                return new JsonResult($"Intente nuevamente con un parametro distinto a {Rol}");
            }
            else
            {
                return new JsonResult(rolRetornado);
            }
        }

        [HttpPost]
        public void AgregarRol([FromBody] Rol rol)
        {
            RolAzure.AgregarRol(rol);
        }

        [HttpDelete("{id}")]
        public void EliminarRol(int id)
        {
            RolAzure.EliminarRolPorId(id);
        }

        [HttpPut]
        public void editarRol([FromBody] Rol rol)
        {
            RolAzure.ActualizarRol(rol);
        }
    }
}
