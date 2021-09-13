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
    public class ParcelaController : ControllerBase
    {
        private readonly IParcelaService parcelaService;
        private readonly IContratoService contratoService;
        public ParcelaController(IParcelaService parcelaService, IContratoService contratoService)
        {
            this.parcelaService = parcelaService;
            this.contratoService = contratoService;
        }

        [HttpGet("contrato/{numero}")]
        public async Task<IActionResult> GetByNumeroContrato(string numeroContrato)
        {
            try
            {
                var contrato = await contratoService.GetContratoByNumeroAsync(numeroContrato);
                if (contrato == null) return NoContent();

                var parcelas = await parcelaService.GetAllParcelasByContratoAsync(contrato.Id);
                if (parcelas == null) return NoContent();

                return Ok(parcelas);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar parcelas. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var parcela = await parcelaService.GetParcelaById(id);
                if (parcela == null) return NoContent();

                return Ok(parcela);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar parcela. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ParcelaDto model)
        {
            try
            {
                var parcela = await parcelaService.AddParcela(model);
                if (parcela == null) return NoContent();

                return Ok(parcela);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar parcela. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ParcelaDto model)
        {
            try
            {
                var parcela = await parcelaService.UpdateParcela(id, model);
                if (parcela == null) return NoContent();

                return Ok(parcela);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar atualizar parcela. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await parcelaService.DeleteParcela(id) ?
                            Ok("Deletada.") :
                            BadRequest("Parcela não deletada.");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar deletar parcela. Erro: {ex.Message}");
                throw;
            }
        }
    }
}