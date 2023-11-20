using bdd_obligatorio_api.Models;
using bdd_obligatorio_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace bdd_obligatorio_api.Controllers
{
    [ApiController]
    [Route("api/periodoactual")]
    public class Periodo_ActualizacionController : ControllerBase
    {
        private readonly IPeriodoActualizacionRepository _periodoActualizacionRepository;

        public Periodo_ActualizacionController(IPeriodoActualizacionRepository periodoActualizacionRepository)
        {
            _periodoActualizacionRepository = periodoActualizacionRepository;
        }

        [HttpGet]
        public IActionResult GetPeriodoActual()
        {
            try
            {
                var periodoActual = _periodoActualizacionRepository.GetPeriodoActual();
                if (periodoActual != null)
                {
                    return Ok(periodoActual);
                }
                else
                {
                    return NotFound("Periodo de actualización no encontrado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el periodo de actualización: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult UpdatePeriodoActual([FromBody] Periodo_Actualizacion periodo)
        {
            try
            {
                _periodoActualizacionRepository.UpdatePeriodoActual(periodo);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar el periodo de actualización: {ex.Message}");
            }
        }
    }
}
