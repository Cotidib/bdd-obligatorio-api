using bdd_obligatorio_api.Models;
using bdd_obligatorio_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace bdd_obligatorio_api.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioController(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        [HttpPost]
        public IActionResult AddFuncionario([FromBody] Funcionario funcionario)
        {
            try
            {
                // Verificar si el funcionario ya existe
                var existingFuncionario = _funcionarioRepository.GetFuncionarioByCi(funcionario.Ci);
                if (existingFuncionario != null)
                {
                    return Conflict($"Ya existe un funcionario con CI {funcionario.Ci}");
                }

                _funcionarioRepository.AddFuncionario(funcionario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agregar el funcionario: {ex.Message}");
            }
        }

        [HttpGet("{ci}")]
        public IActionResult GetFuncionarioByCi(int ci)
        {
            try
            {
                var funcionario = _funcionarioRepository.GetFuncionarioByCi(ci);
                if (funcionario != null)
                {
                    return Ok(funcionario);
                }
                else
                {
                    return NotFound($"Funcionario con CI {ci} no encontrado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el funcionario: {ex.Message}");
            }
        }
    }
}
