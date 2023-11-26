using System.Security.Claims;
using bdd_obligatorio_api.Contracts.DB;
using bdd_obligatorio_api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

[ApiController]
[Route("[controller]")]
public class ArchivosController : ControllerBase
{
    private readonly MySqlConnection _mySqlConnection;
    private readonly string _connectionString;

    public ArchivosController(MySqlConnection mySqlConnection, IConfiguration configuration)
    {
        _mySqlConnection = mySqlConnection;
        _connectionString = configuration.GetConnectionString("Default");
    }
    // [Authorize]
    // [HttpPost("uploadFile")]
    // public async Task<IActionResult> SubirArchivo(UploadRequest uploadRequest)
    // {
    //     try
    //     {
    //         using (var connection = new MySqlConnection(_connectionString))
    //         {
    //             await connection.OpenAsync();

    //             using (var command = connection.CreateCommand())
    //             {
    //                 //Convertir el archivo a un array de bytes
    //                 byte[] fileBytes;
    //                 using (var memoryStream = new MemoryStream())
    //                 {
    //                     await uploadRequest.comprobante.CopyToAsync(memoryStream);
    //                     fileBytes = memoryStream.ToArray();
    //                 }

    //                 //Parámetros para la consulta
    //                 command.CommandText = "INSERT INTO Comprobante (Cid, Nombre, Contenido, Tipo) VALUES (@Cid, @Nombre, @Contenido, @Tipo)";
    //                 command.Parameters.AddWithValue("@Cid", uploadRequest.ci);
    //                 command.Parameters.AddWithValue("@Nombre", uploadRequest.comprobante.FileName);
    //                 command.Parameters.AddWithValue("@Contenido", fileBytes);
    //                 command.Parameters.AddWithValue("@Tipo", uploadRequest.comprobante.ContentType);

    //                 //Ejecutar la consulta
    //                 await command.ExecuteNonQueryAsync();
    //                 Console.WriteLine("Archivo subido");
    //             }
    //         }
    //         using (var connection = new MySqlConnection(_connectionString))
    //         {
    //             await connection.OpenAsync();

    //             using (var command = connection.CreateCommand())
    //             {

    //                 //Parámetros para la consulta
    //                 command.CommandText = "INSERT INTO Carnet_Salud (Ci, Fch_Emision, Fch_Vencimiento, Comprobante) VALUES (@Ci, @Fch_Emision, @Fch_Vencimiento, @Comprobante)";
    //                 command.Parameters.AddWithValue("@Ci", uploadRequest.ci);
    //                 command.Parameters.AddWithValue("@Fch_Emision", uploadRequest.F);
    //                 command.Parameters.AddWithValue("@Fch_Vencimiento", fileBytes);
    //                 command.Parameters.AddWithValue("@Comprobante", uploadRequest.comprobante.ContentType);
    //                 //Ejecutar la consulta
    //                 await command.ExecuteNonQueryAsync();
    //             }
    //         }

    //         return Ok(new { mensaje = "Archivo subido con éxito" });
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(500, new { mensaje = $"Error al subir el archivo: {ex.Message}" });
    //     }
    // }

    [Authorize]
    [HttpPost("uploadSignup")]
    public async Task<IActionResult> uploadSignup([FromForm] SignupRequest signupRequest, [FromForm] IFormFile comprobante)
    {
        try
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = await connection.BeginTransactionAsync())
                {

                    try
                    {                                                                   //Falta el LOGID!!!
                        Console.WriteLine("ACA EMPIEZA LA TRANSACC: ");
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "INSERT INTO Funcionarios (Ci, Nombre, Apellido, Email, Fch_Nacimiento, Direccion, Telefono, Logid) VALUES (@Ci, @Nombre, @Apellido, @Email, @Fch_Nacimiento, @Direccion, @Telefono, @Logid)";
                            command.Parameters.AddWithValue("@Ci", Int32.Parse(signupRequest.ci));
                            command.Parameters.AddWithValue("@Nombre", signupRequest.nombre);
                            command.Parameters.AddWithValue("@Apellido", signupRequest.apellido);
                            command.Parameters.AddWithValue("@Email", signupRequest.email);
                            command.Parameters.AddWithValue("@Fch_Nacimiento", signupRequest.fechaNacimiento);
                            command.Parameters.AddWithValue("@Direccion", signupRequest.domicilio);
                            command.Parameters.AddWithValue("@Telefono", Int32.Parse(signupRequest.telefono));
                            command.Parameters.AddWithValue("@Logid", GetUserId().ToString());


                            command.Transaction = transaction;

                            //Ejecutar la consulta
                            await command.ExecuteNonQueryAsync();
                            Console.WriteLine("Archivo subido");
                        }
                        if (signupRequest.tieneCarneSalud)
                        {
                            using (var command = connection.CreateCommand())
                            {
                                //Convertir el archivo a un array de bytes
                                byte[] fileBytes;
                                using (var memoryStream = new MemoryStream())
                                {
                                    await comprobante.CopyToAsync(memoryStream);
                                    fileBytes = memoryStream.ToArray();
                                }

                                //Parámetros para la consulta
                                command.CommandText = "INSERT INTO Comprobante (Cid, Nombre, Contenido, Tipo) VALUES (@Cid, @Nombre, @Contenido, @Tipo)";
                                command.Parameters.AddWithValue("@Cid", signupRequest.ci);
                                command.Parameters.AddWithValue("@Nombre", comprobante.FileName);
                                command.Parameters.AddWithValue("@Contenido", fileBytes);
                                command.Parameters.AddWithValue("@Tipo", comprobante.ContentType);
                                command.Transaction = transaction;

                                //Ejecutar la consulta
                                await command.ExecuteNonQueryAsync();
                                Console.WriteLine("Archivo subido");
                            }
                        }
                        using (var command = connection.CreateCommand())
                        {

                            //Parámetros para la consulta
                            command.CommandText = "INSERT INTO Carnet_Salud (Ci, Fch_Emision, Fch_Vencimiento, Comprobante) VALUES (@Ci, @Fch_Emision, @Fch_Vencimiento, @Comprobante)";
                            command.Parameters.AddWithValue("@Ci", signupRequest.ci);
                            command.Parameters.AddWithValue("@Fch_Emision", signupRequest.fechaEmision);
                            command.Parameters.AddWithValue("@Fch_Vencimiento", signupRequest.fechaVencimiento);
                            command.Parameters.AddWithValue("@Comprobante", signupRequest.ci);
                            command.Transaction = transaction;
                            //Ejecutar la consulta
                            await command.ExecuteNonQueryAsync();
                        }
                        await transaction.CommitAsync();
                        return Ok(new { mensaje = "Archivo subido con éxito" });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex}");
                        await transaction.RollbackAsync();
                        return StatusCode(500, new { mensaje = $"ROLLBACK: Error al subir el archivo: {ex.Message}" });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensaje = $"Error al subir el archivo: {ex.Message}" });
        }
    }
    public string GetUserId()
    {
        try
        {
            // Obtener el principal del usuario desde el contexto HTTP
            var principal = HttpContext.User;

            // Buscar la claim con el tipo "UserId"
            var userIdClaim = principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;
                Console.WriteLine(userId);
                return userId;
            }
            else
            {
                Console.WriteLine("No se encontró el GetUserID");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}