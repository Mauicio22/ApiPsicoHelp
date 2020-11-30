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
    public class UsuarioController : ControllerBase
    {
        
        //GET /api/usuario/all
        [HttpGet("all")]
        public JsonResult ObtenerUsuarios()
        {
            //json
            var usuariosRetornados = UsuarioAzure.obtenerUsuarios();
            return new JsonResult(usuariosRetornados);
        }

        //GET /api/usuario/rut
        [HttpGet("{Usuario}")]
        public JsonResult ObtenerUsuario(string Usuario)
        {
            var conversionExitosa = int.TryParse(Usuario, out int idConvertido);

            Models.Usuario usuarioRetornado;

            if (conversionExitosa)
            {
                usuarioRetornado = UsuarioAzure.ObtenerUsuario(idConvertido);
            }
            else
            {
                usuarioRetornado = UsuarioAzure.ObtenerUsuario(Usuario);
            }
            if(usuarioRetornado is null)
            {
                return new JsonResult($"Intente nuevamente con un parametro distinto a {Usuario}");
            }
            else
            {
                return new JsonResult(usuarioRetornado);
            }
        }
        //POST: api/usuario
        [HttpPost]
        public void AgregarUsuario([FromBody] Usuario usuario)
        {
            UsuarioAzure.AgregarUsuario(usuario);
        }

        [HttpDelete("{rut}")]
        public void EliminarUsuario(string rut)
        {
            UsuarioAzure.EliminarUsuarioPorRut(rut);
        }

        [HttpPut]
        public void editarUsuario([FromBody] Usuario usuario)
        {
            UsuarioAzure.ActualizarUsuario(usuario);
        }



    }
}
