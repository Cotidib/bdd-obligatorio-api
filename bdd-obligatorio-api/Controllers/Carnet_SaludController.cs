using bdd_obligatorio_api.Models;
using bdd_obligatorio_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace bdd_obligatorio_api.Controllers
{
    [ApiController]
    [Route("api/carnetsalud")]
    public class Carnet_SaludController : ControllerBase
    {
        private readonly ICarnetSaludRepository _carnetSaludRepository;

        public Carnet_SaludController(ICarnetSaludRepository carnetSaludRepository)
        {
            _carnetSaludRepository = carnetSaludRepository;
        }

        [HttpPost]
        public IActionResult AddCarnetSalud([FromBody] Carnet_Salud carnetSalud)
        {
            try
            {
                _carnetSaludRepository.AddCarnetSalud(carnetSalud);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agregar el carné de salud: {ex.Message}");
            }
        }
    }
}
