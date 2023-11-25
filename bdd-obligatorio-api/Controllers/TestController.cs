using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace bdd_obligatorio_api.Controllers;

[ApiController]
public class ArchivosController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("/test")]
    public IActionResult testGet()
    {
        try
        {
            return Ok(new { mensaje = "Prueba exitosa" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = $"Error en la prueba: {ex.Message}" });
        }
    }
}