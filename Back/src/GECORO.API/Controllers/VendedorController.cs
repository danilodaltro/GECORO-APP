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
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorService vendedorService;
        public VendedorController(IVendedorService vendedorService)
        {
            this.vendedorService = vendedorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var vendedores = await vendedorService.GetAllVendedoresAsync(true);
                if (vendedores == null) return NoContent();

                return Ok(vendedores);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar vendedores. Erro: {ex.Message}");
            }
        }

        [HttpGet("nome/{nome}")]
        public async Task<IActionResult> GetByNome(string nome)
        {
            try
            {
                var vendedores = await vendedorService.GetAllVendedoresByNomeAsync(nome,true);
                if (vendedores == null) return NoContent();

                return Ok(vendedores);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar vendedores. Erro: {ex.Message}");
            }
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            try
            {
                var vendedor = await vendedorService.GetVendedorByCodigoAsync(codigo,true);
                if (vendedor == null) return NoContent();

                return Ok(vendedor);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar vendedor. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var vendedor = await vendedorService.GetVendedorByIdAsync(id,true);
                if (vendedor == null) return NoContent();

                return Ok(vendedor);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar vendedor. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(VendedorDto model)
        {
            try
            {
                var vendedor = await vendedorService.AddVendedor(model);
                if (vendedor == null) return NoContent();

                return Ok(vendedor);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar vendedor. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, VendedorDto model)
        {
            try
            {
                var vendedor = await vendedorService.UpdateVendedor(id, model);
                if (vendedor == null) return NoContent();

                return Ok(vendedor);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar atualizar vendedor. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await vendedorService.DeleteVendedor(id) ?
                            Ok("Deletado.") :
                            BadRequest("Vendedor não deletado.");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar deletar vendedor. Erro: {ex.Message}");
                throw;
            }
        }
    }
}