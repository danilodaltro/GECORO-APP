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
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService contratoService;
        public ContratoController(IContratoService contratoService)
        {
            this.contratoService = contratoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var contratos = await contratoService.GetAllContratosAsync();
                if (contratos == null) return NoContent();

                return Ok(contratos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar contratos. Erro: {ex.Message}");
            }
        }

        [HttpGet("vendedor/{id}")]
        public async Task<IActionResult> GetByVendedor(int id)
        {
            try
            {
                var contratos = await contratoService.GetAllContratosByVendedorAsync(id);
                if (contratos == null) return NoContent();

                return Ok(contratos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar contratos. Erro: {ex.Message}");
            }
        }

        [HttpGet("cliente/{idCliente}")]
        public async Task<IActionResult> GetByCliente(int idCliente)
        {
            try
            {
                var contratos = await contratoService.GetAllContratosByClienteAsync(idCliente);
                if (contratos == null) return NoContent();

                return Ok(contratos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar contratos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var contratos = await contratoService.GetContratoByIdAsync(id);
                if (contratos == null) return NoContent();

                return Ok(contratos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar contrato. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContratoDto model)
        {
            try
            {
                var contrato = await contratoService.AddContrato(model);
                if (contrato == null) return NoContent();

                return Ok(contrato);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar contrato. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ContratoDto model)
        {
            try
            {
                var contrato = await contratoService.UpdateContrato(id, model);
                if (contrato == null) return NoContent();

                return Ok(contrato);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar atualizar contrato. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await contratoService.DeleteContrato(id) ?
                            Ok("Deletado.") :
                            BadRequest("Contrato não deletado.");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar deletar contrato. Erro: {ex.Message}");
                throw;
            }
        }
    }
}