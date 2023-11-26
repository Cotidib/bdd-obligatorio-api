using bdd_obligatorio_api.Models;
using bdd_obligatorio_api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace bdd_obligatorio_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaRepository _agendaRepository;

        public AgendaController(IAgendaRepository agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }
        [Authorize]
        [HttpPost("/agenda")]
        public IActionResult AddAgenda([FromForm] Agenda agenda)
        {
            try
            {
                _agendaRepository.AddAgenda(agenda);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agregar la agenda: {ex.Message}");
            }
        }
    }
}
