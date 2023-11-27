using System.Security.Claims;
using bdd_obligatorio_api.Contracts.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

[ApiController]
[Route("[controller]")]
public class ArchivosController : ControllerBase
{
    private readonly MySqlConnection _mySqlConnection;
    private readonly string _connectionString;
    private DateTime currentDate = DateTime.Now;

    public ArchivosController(MySqlConnection mySqlConnection, IConfiguration configuration)
    {
        _mySqlConnection = mySqlConnection;
        _connectionString = configuration.GetConnectionString("Default");
    }
    [Authorize]
    [HttpPost("uploadFile")]
    public async Task<IActionResult> SubirArchivo([FromForm] UploadRequest uploadRequest, [FromForm] IFormFile? comprobante)
    {
        try
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = await connection.BeginTransactionAsync())
                {

                    try
                    {
                        bool funcionarioExist = false;
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "SELECT * FROM Funcionarios WHERE Ci = @Ci";
                            command.Parameters.AddWithValue("@Ci", Int32.Parse(uploadRequest.ci));
                            command.Transaction = transaction;
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                funcionarioExist = await reader.ReadAsync();
                            }
                        }
                        if (!funcionarioExist)
                        {
                            return Ok(new { redirectUrl = "/signup-form", mensaje = "Funcionario inexistente. Por favor, complete el formulario de alta." });
                        }

                        Console.WriteLine("ACA EMPIEZA LA TRANSACC: ");
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = "UPDATE Funcionarios SET Nombre = @Nombre, Apellido = @Apellido, Fch_Nacimiento = @Fch_Nacimiento WHERE Ci = @Ci AND Logid = @Logid";
                            command.Parameters.AddWithValue("@Ci", Int32.Parse(uploadRequest.ci));
                            command.Parameters.AddWithValue("@Nombre", uploadRequest.nombre);
                            command.Parameters.AddWithValue("@Apellido", uploadRequest.apellido);
                            command.Parameters.AddWithValue("@Fch_Nacimiento", uploadRequest.fechaNacimiento);
                            command.Parameters.AddWithValue("@Logid", GetUserId().ToString());

                            command.Transaction = transaction;

                            await command.ExecuteNonQueryAsync();
                            Console.WriteLine("Datos Actualizados");
                        }
                        if (uploadRequest.tieneCarneSalud)
                        {
                            if (uploadRequest.fechaVencimiento <= currentDate)
                            {
                                await transaction.CommitAsync();
                                return Ok(new { redirectUrl = "/agenda", mensaje = "Su carnet está vencido. Por favor, agende una consulta para el carnet de salud." });
                            }
                            bool carnetExists = false;
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = "SELECT * FROM Comprobante WHERE Cid = @Cid";
                                command.Parameters.AddWithValue("@Cid", Int32.Parse(uploadRequest.ci));
                                command.Transaction = transaction;
                                using (var reader = await command.ExecuteReaderAsync())
                                {
                                    carnetExists = await reader.ReadAsync();
                                }
                            }
                            if (!carnetExists)
                            {
                                using (var command = connection.CreateCommand())
                                {
                                    byte[] fileBytes;
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        await comprobante.CopyToAsync(memoryStream);
                                        fileBytes = memoryStream.ToArray();
                                    }

                                    command.CommandText = "INSERT INTO Comprobante (Cid, Nombre, Contenido, Tipo) VALUES (@Cid, @Nombre, @Contenido, @Tipo)";
                                    command.Parameters.AddWithValue("@Cid", uploadRequest.ci);
                                    command.Parameters.AddWithValue("@Nombre", comprobante.FileName);
                                    command.Parameters.AddWithValue("@Contenido", fileBytes);
                                    command.Parameters.AddWithValue("@Tipo", comprobante.ContentType);
                                    command.Transaction = transaction;

                                    await command.ExecuteNonQueryAsync();
                                    Console.WriteLine("Archivo subido");
                                }

                                using (var command = connection.CreateCommand())
                                {
                                    command.CommandText = "INSERT INTO Carnet_Salud (Ci, Fch_Emision, Fch_Vencimiento, Comprobante) VALUES (@Ci, @Fch_Emision, @Fch_Vencimiento, @Comprobante)";
                                    command.Parameters.AddWithValue("@Ci", uploadRequest.ci);
                                    command.Parameters.AddWithValue("@Fch_Emision", uploadRequest.fechaEmision);
                                    command.Parameters.AddWithValue("@Fch_Vencimiento", uploadRequest.fechaVencimiento);
                                    command.Parameters.AddWithValue("@Comprobante", uploadRequest.ci);
                                    command.Transaction = transaction;
                                    await command.ExecuteNonQueryAsync();
                                }
                            }
                            else
                            {
                                using (var command = connection.CreateCommand())
                                {
                                    byte[] fileBytes;
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        await comprobante.CopyToAsync(memoryStream);
                                        fileBytes = memoryStream.ToArray();
                                    }
                                    command.CommandText = "UPDATE Comprobante SET Nombre = @Nombre, Contenido = @Contenido, Tipo = @Tipo WHERE Cid = @Cid";
                                    command.Parameters.AddWithValue("@Cid", uploadRequest.ci);
                                    command.Parameters.AddWithValue("@Nombre", comprobante.FileName);
                                    command.Parameters.AddWithValue("@Contenido", fileBytes);
                                    command.Parameters.AddWithValue("@Tipo", comprobante.ContentType);
                                    command.Transaction = transaction;

                                    await command.ExecuteNonQueryAsync();
                                    Console.WriteLine("Archivo actualizado");
                                }

                                using (var command = connection.CreateCommand())
                                {
                                    command.CommandText = "UPDATE Carnet_Salud SET Fch_Emision = @Fch_Emision, Fch_Vencimiento = @Fch_Vencimiento, Comprobante = @Comprobante WHERE Ci = @Ci";
                                    command.Parameters.AddWithValue("@Ci", uploadRequest.ci);
                                    command.Parameters.AddWithValue("@Fch_Emision", uploadRequest.fechaEmision);
                                    command.Parameters.AddWithValue("@Fch_Vencimiento", uploadRequest.fechaVencimiento);
                                    command.Parameters.AddWithValue("@Comprobante", uploadRequest.ci);
                                    command.Transaction = transaction;
                                    await command.ExecuteNonQueryAsync();
                                }
                            }
                        }
                        else
                        {
                            await transaction.CommitAsync();
                            return Ok(new { redirectUrl = "/agenda", mensaje = "Datos actualizados. Por favor, agende una consulta para el carnet de salud." });
                        }
                        await transaction.CommitAsync();
                        return Ok(new { mensaje = "Datos actualizados con éxito" });
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

    [Authorize]
    [HttpPost("uploadSignup")]
    public async Task<IActionResult> uploadSignup([FromForm] SignupRequest signupRequest, [FromForm] IFormFile? comprobante)
    {
        try
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = await connection.BeginTransactionAsync())
                {

                    try
                    {
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

                            await command.ExecuteNonQueryAsync();
                            Console.WriteLine("Archivo subido");
                        }
                        if (signupRequest.tieneCarneSalud)
                        {
                            if (signupRequest.fechaVencimiento <= currentDate)
                            {
                                await transaction.CommitAsync();
                                return Ok(new { redirectUrl = "/agenda", mensaje = "Su carnet está vencido. Por favor, agende una consulta para el carnet de salud." });
                            }
                            using (var command = connection.CreateCommand())
                            {
                                byte[] fileBytes;
                                using (var memoryStream = new MemoryStream())
                                {
                                    await comprobante.CopyToAsync(memoryStream);
                                    fileBytes = memoryStream.ToArray();
                                }
                                command.CommandText = "INSERT INTO Comprobante (Cid, Nombre, Contenido, Tipo) VALUES (@Cid, @Nombre, @Contenido, @Tipo)";
                                command.Parameters.AddWithValue("@Cid", signupRequest.ci);
                                command.Parameters.AddWithValue("@Nombre", comprobante.FileName);
                                command.Parameters.AddWithValue("@Contenido", fileBytes);
                                command.Parameters.AddWithValue("@Tipo", comprobante.ContentType);
                                command.Transaction = transaction;
                                await command.ExecuteNonQueryAsync();
                                Console.WriteLine("Archivo subido");
                            }

                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = "INSERT INTO Carnet_Salud (Ci, Fch_Emision, Fch_Vencimiento, Comprobante) VALUES (@Ci, @Fch_Emision, @Fch_Vencimiento, @Comprobante)";
                                command.Parameters.AddWithValue("@Ci", signupRequest.ci);
                                command.Parameters.AddWithValue("@Fch_Emision", signupRequest.fechaEmision);
                                command.Parameters.AddWithValue("@Fch_Vencimiento", signupRequest.fechaVencimiento);
                                command.Parameters.AddWithValue("@Comprobante", signupRequest.ci);
                                command.Transaction = transaction;
                                await command.ExecuteNonQueryAsync();
                            }
                        }
                        else
                        {
                            await transaction.CommitAsync();
                            return Ok(new { redirectUrl = "/agenda", mensaje = "Datos actualizados. Por favor, agende una consulta para el carnet de salud." });
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
            var principal = HttpContext.User;

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