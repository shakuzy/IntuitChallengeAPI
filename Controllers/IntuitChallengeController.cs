using IntuitChallengeAPI.Clases.API.Request;
using IntuitChallengeAPI.Clases.API.Response;
using IntuitChallengeAPI.Clases.DTO;
using IntuitChallengeAPI.Context;
using IntuitChallengeAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntuitChallengeAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class IntuitChallengeController : ControllerBase
    {
        private IntuitChallengeContext _db;
        private Clases.Injections.ILogger _logger;
        public IntuitChallengeController(IntuitChallengeContext context, Clases.Injections.ILogger logger)
        {
            _db = context;
            _logger = logger;
        }
        [HttpGet("GetAll")]
        public async Task<GetAllResponse> GetAll()
        {
            try
            {
                List<ClienteDTO> resultado = new List<ClienteDTO>();

                List<Cliente> clientes = await _db.Clientes.ToListAsync();

                foreach (Cliente cliente in clientes)
                    resultado.Add(cliente.Convertir());

                return new GetAllResponse("OK", resultado);
            }
            catch (Exception ex)
            {
                string msjError = $"Ocurrio un error inesperado: {ex.Message}.";
                await _logger.GuardarError(msjError);
                return new GetAllResponse("ERROR", msjError);
            }
        }
        [HttpGet("Get")]
        public async Task<GetResponse> Get(int idCliente)
        {
            try
            {
                if (idCliente == 0)
                {
                    string msjError = "idCliente es 0.";
                    await _logger.GuardarError(msjError);
                    return new GetResponse("ERROR", msjError);
                }

                Cliente? cliente = await _db.Clientes.Where(x => x.Id == idCliente).FirstOrDefaultAsync();

                if (cliente == null)
                {
                    string msjError = $"No existe ningun cliente con Id: {idCliente}.";
                    await _logger.GuardarError(msjError);
                    return new GetResponse("ERROR", msjError);
                }

                return new GetResponse("OK", cliente.Convertir());
            }
            catch (Exception ex)
            {
                string msjError = $"Ocurrio un error inesperado: {ex.Message}.";
                await _logger.GuardarError(msjError);
                return new GetResponse("ERROR", msjError);
            }
        }
        [HttpGet("Search")]
        public async Task<SearchResponse> Search(string nombreCliente)
        {
            try
            {
                if (string.IsNullOrEmpty(nombreCliente))
                {
                    string msjError = "nombreCliente es nulo o vacio.";
                    await _logger.GuardarError(msjError);
                    return new SearchResponse("ERROR", msjError);
                }

                Cliente? cliente = await _db.Clientes.Where(x => x.Nombres.Contains(nombreCliente)).FirstOrDefaultAsync();

                if (cliente == null)
                {
                    string msjError = $"No existe ningun cliente con Nombre: {nombreCliente}.";
                    await _logger.GuardarError(msjError);
                    return new SearchResponse("ERROR", msjError);
                }

                return new SearchResponse("OK", cliente.Convertir());
            }
            catch (Exception ex)
            {
                string msjError = $"Ocurrio un error inesperado: {ex.Message}.";
                await _logger.GuardarError(msjError);
                return new SearchResponse("ERROR", msjError);
            }
        }
        [HttpDelete("Delete")]
        public async Task<DeleteResponse> Delete(int idCliente)
        {
            try
            {
                if (idCliente == 0)
                {
                    string msjError = "idCliente es 0.";
                    await _logger.GuardarError(msjError);
                    return new DeleteResponse("ERROR", msjError);
                }

                Cliente? cliente = await _db.Clientes.Where(x => x.Id == idCliente).FirstOrDefaultAsync();

                if (cliente == null)
                {
                    string msjError = $"No existe ningun cliente con Id: {idCliente}.";
                    await _logger.GuardarError(msjError);
                    return new DeleteResponse("ERROR", msjError);
                }

                _db.Clientes.Remove(cliente);

                if (await _db.SaveChangesAsync() < 1)
                {
                    string msjError = $"Ocurrio un error al eliminar el cliente.";
                    await _logger.GuardarError(msjError);
                    return new DeleteResponse("ERROR", msjError);
                }

                return new DeleteResponse("OK");
            }
            catch (Exception ex)
            {
                string msjError = $"Ocurrio un error inesperado: {ex.Message}.";
                await _logger.GuardarError(msjError);
                return new DeleteResponse("ERROR", msjError);
            }
        }
        [HttpPost("Insert")]
        public async Task<InsertResponse> Insert(InsertRequest request)
        {
            try
            {
                if (request == null)
                {
                    string msjError = "request es null.";
                    await _logger.GuardarError(msjError);
                    return new InsertResponse("ERROR", msjError);
                }

                if (request.Cliente == null)
                {
                    string msjError = "request.Cliente es null.";
                    await _logger.GuardarError(msjError);
                    return new InsertResponse("ERROR", msjError);
                }

                if (string.IsNullOrEmpty(request.Cliente.Nombres))
                {
                    string msjError = "request.Cliente.Nombres es null o vacio.";
                    await _logger.GuardarError(msjError);
                    return new InsertResponse("ERROR", msjError);
                }
                if (string.IsNullOrEmpty(request.Cliente.Apellidos))
                {
                    string msjError = "request.Cliente.Apellidos es null o vacio.";
                    await _logger.GuardarError(msjError);
                    return new InsertResponse("ERROR", msjError);
                }
                if (string.IsNullOrEmpty(request.Cliente.Cuit))
                {
                    string msjError = "request.Cliente.Cuit es null o vacio.";
                    await _logger.GuardarError(msjError);
                    return new InsertResponse("ERROR", msjError);
                }
                if (request.Cliente.Cuit.Length != 11)
                {
                    string msjError = "request.Cliente.Cuit no tiene un formato valido.";
                    await _logger.GuardarError(msjError);
                    return new InsertResponse("ERROR", msjError);
                }
                if (string.IsNullOrEmpty(request.Cliente.TelefonoCelular))
                {
                    string msjError = "request.Cliente.TelefonoCelular es null o vacio.";
                    await _logger.GuardarError(msjError);
                    return new InsertResponse("ERROR", msjError);
                }
                if (string.IsNullOrEmpty(request.Cliente.Email))
                {
                    string msjError = "request.Cliente.Email es null o vacio.";
                    await _logger.GuardarError(msjError);
                    return new InsertResponse("ERROR", msjError);
                }
                if (!request.Cliente.Email.Contains("@"))
                {
                    string msjError = "request.Cliente.Email no tiene un formato valido.";
                    await _logger.GuardarError(msjError);
                    return new InsertResponse("ERROR", msjError);
                }

                if (await ExisteUsuario(0, request.Cliente.Cuit))
                {
                    string msjError = $"Ya existe un usuario con Cuit: {request.Cliente.Cuit}.";
                    await _logger.GuardarError(msjError);
                    return new InsertResponse("ERROR", msjError);
                }

                Cliente clienteNuevo = new()
                {
                    Apellidos = request.Cliente.Apellidos,
                    Cuit = request.Cliente.Cuit,
                    Domicilio = request.Cliente.Domicilio,
                    Email = request.Cliente.Email,
                    FechaNacimiento = request.Cliente.FechaNacimiento,
                    Nombres = request.Cliente.Nombres,
                    TelefonoCelular = request.Cliente.TelefonoCelular
                };

                await _db.Clientes.AddAsync(clienteNuevo);
                if (await _db.SaveChangesAsync() < 1)
                {
                    string msjError = $"Ocurrio un error al agregar el cliente.";
                    await _logger.GuardarError(msjError);
                    return new InsertResponse("ERROR", msjError);
                }

                return new InsertResponse("OK", clienteNuevo.Id);
            }
            catch (Exception ex)
            {
                string msjError = $"Ocurrio un error inesperado: {ex.Message}.";
                await _logger.GuardarError(msjError);
                return new InsertResponse("ERROR", msjError);
            }
        }
        [HttpPut("Update")]
        public async Task<UpdateResponse> Update(UpdateRequest request)
        {
            try
            {
                if (request == null)
                {
                    string msjError = "request es null.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }

                if (request.Cliente == null)
                {
                    string msjError = "request.Cliente es null.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }

                if (request.Cliente.Id == 0)
                {
                    string msjError = "request.Cliente.Id es 0.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }
                if (string.IsNullOrEmpty(request.Cliente.Nombres))
                {
                    string msjError = "request.Cliente.Nombres es null o vacio.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }
                if (string.IsNullOrEmpty(request.Cliente.Apellidos))
                {
                    string msjError = "request.Cliente.Apellidos es null o vacio.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }
                if (string.IsNullOrEmpty(request.Cliente.Cuit))
                {
                    string msjError = "request.Cliente.Cuit es null o vacio.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }
                if (request.Cliente.Cuit.Length != 11)
                {
                    string msjError = "request.Cliente.Cuit no tiene un formato valido.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }
                if (string.IsNullOrEmpty(request.Cliente.TelefonoCelular))
                {
                    string msjError = "request.Cliente.TelefonoCelular es null o vacio.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }
                if (string.IsNullOrEmpty(request.Cliente.Email))
                {
                    string msjError = "request.Cliente.Email es null o vacio.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }
                if (!request.Cliente.Email.Contains("@"))
                {
                    string msjError = "request.Cliente.Email no tiene un formato valido.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }


                Cliente? cliente = await _db.Clientes.Where(x => x.Id == request.Cliente.Id).FirstOrDefaultAsync();

                if (cliente == null)
                {
                    string msjError = $"No existe ningun cliente con Id: {request.Cliente.Id}.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }

                if (await ExisteUsuario(request.Cliente.Id, request.Cliente.Cuit))
                {
                    string msjError = $"Ya existe un usuario con Cuit: {request.Cliente.Cuit}.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }

                if (cliente.Apellidos == request.Cliente.Apellidos && cliente.Cuit == request.Cliente.Cuit &&
                cliente.Domicilio == request.Cliente.Domicilio &&
                cliente.Email == request.Cliente.Email &&
                cliente.FechaNacimiento == request.Cliente.FechaNacimiento &&
                cliente.Nombres == request.Cliente.Nombres &&
                cliente.TelefonoCelular == request.Cliente.TelefonoCelular)
                    return new UpdateResponse("OK");
                
                cliente.Apellidos = request.Cliente.Apellidos;
                cliente.Cuit = request.Cliente.Cuit;
                cliente.Domicilio = request.Cliente.Domicilio;
                cliente.Email = request.Cliente.Email;
                cliente.FechaNacimiento = request.Cliente.FechaNacimiento;
                cliente.Nombres = request.Cliente.Nombres;
                cliente.TelefonoCelular = request.Cliente.TelefonoCelular;

                
                if (await _db.SaveChangesAsync() < 1)
                {
                    string msjError = $"Ocurrio un error al editar el cliente.";
                    await _logger.GuardarError(msjError);
                    return new UpdateResponse("ERROR", msjError);
                }

                return new UpdateResponse("OK");
            }
            catch (Exception ex)
            {
                string msjError = $"Ocurrio un error inesperado: {ex.Message}.";
                await _logger.GuardarError(msjError);
                return new UpdateResponse("ERROR", msjError);
            }
        }
        private async Task<bool> ExisteUsuario(int idUsuario, string cuit)
        {
            try
            {
                Cliente? clienteExistente = await _db.Clientes.Where(x => x.Cuit == cuit && (idUsuario == 0 || x.Id != idUsuario)).FirstOrDefaultAsync();

                if (clienteExistente != null)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                string msjError = $"Ocurrio un error inesperado: {ex.Message}.";
                await _logger.GuardarError(msjError);
                return true;
            }
        }
    }
}
