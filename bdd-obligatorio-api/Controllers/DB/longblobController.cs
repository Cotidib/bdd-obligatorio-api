// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using MySql.Data.MySqlClient;
// using System;
// using System.Data;
// using System.IO;
// using System.Threading.Tasks;
// /*


// Hay que modificar este código


// */
// [ApiController]
// [Route("api/archivos")]
// public class ArchivosController : ControllerBase
// {
//     private readonly string _connectionString = "tu cadena de conexión MySQL";

//     [HttpPost]
//     public async Task<IActionResult> SubirArchivo(IFormFile file)
//     {
//         try
//         {
//             using (var connection = new MySqlConnection(_connectionString))
//             {
//                 await connection.OpenAsync();

//                 using (var command = connection.CreateCommand())
//                 {
//                     //Convertir el archivo a un array de bytes
//                     byte[] fileBytes;
//                     using (var memoryStream = new MemoryStream())
//                     {
//                         await file.CopyToAsync(memoryStream);
//                         fileBytes = memoryStream.ToArray();
//                     }

//                     //Parámetros para la consulta
//                     command.CommandText = "INSERT INTO Archivos (Nombre, Tipo, Contenido) VALUES (@Nombre, @Tipo, @Contenido)";
//                     command.Parameters.AddWithValue("@Nombre", file.FileName);
//                     command.Parameters.AddWithValue("@Tipo", file.ContentType);
//                     command.Parameters.AddWithValue("@Contenido", fileBytes);

//                     //Ejecutar la consulta
//                     await command.ExecuteNonQueryAsync();
//                 }
//             }

//             return Ok(new { mensaje = "Archivo subido con éxito" });
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, new { mensaje = $"Error al subir el archivo: {ex.Message}" });
//         }
//     }
// }