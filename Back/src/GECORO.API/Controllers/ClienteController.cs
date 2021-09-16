using System;
using System.Threading.Tasks;
using GECORO.Application.Contracts;
using GECORO.Application.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GECORO.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService clienteService;
        public ClienteController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var clientes = await clienteService.GetAllClientesAsync();
                if (clientes == null) return NoContent();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar clientes. Erro: {ex.Message}");
            }
        }

        [HttpGet("vendedor/{id}")]
        public async Task<IActionResult> GetByVendedor(int id)
        {
            try
            {
                var cliente = await clienteService.GetAllClientesByVendedorAsync(id);
                if (cliente == null) return NoContent();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar cliente. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cliente = await clienteService.GetClienteByIdAsync(id);
                if (cliente == null) return NoContent();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar cliente. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteDto model)
        {
            try
            {
                var cliente = await clienteService.AddCliente(model);
                if (cliente == null) return NoContent();

                return Ok(cliente);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar cliente. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClienteDto model)
        {
            try
            {
                var cliente = await clienteService.UpdateCliente(id, model);
                if (cliente == null) return NoContent();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar atualizar cliente. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await clienteService.DeleteCliente(id) ?
                            Ok( new { message = "Deletado"}) :
                            BadRequest("Cliente não deletado.");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar deletar cliente. Erro: {ex.Message}");
                throw;
            }
        }
    }
}