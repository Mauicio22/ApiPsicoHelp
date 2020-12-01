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
    public class CargoController : ControllerBase
    {

        [HttpGet("all")]
        public JsonResult ObtenerCargos()
        {
            //json
            var cargosRetornados = CargoAzure.obtenerCargos();
            return new JsonResult(cargosRetornados);
        }
        [HttpGet("{Cargo}")]
        public JsonResult ObtenerCargoPorId(int Cargo)
        {
           Models.Cargo cargoRetornado;
           cargoRetornado = CargoAzure.obtenerCargoPorId(Cargo);
            
            if (cargoRetornado is null)
            {
                return new JsonResult($"Intente nuevamente con un parametro distinto a {Cargo}");
            }
            else
            {
                return new JsonResult(cargoRetornado);
            }
        }
        
        [HttpPost]
        public void AgregarCargo([FromBody] Cargo cargo)
        {
            CargoAzure.AgregarCargo(cargo);
        }
        [HttpDelete("{id}")]
        public void eliminarCargo(int id)
        {
            CargoAzure.EliminarCargo(id);
        }
        [HttpPut]
        public void editarCargo([FromBody] Cargo cargo)
        {
            CargoAzure.ActualizarCargo(cargo);
        }
    }
    
}
