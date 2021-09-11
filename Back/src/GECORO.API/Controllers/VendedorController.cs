using System;
using System.Threading.Tasks;
using GECORO.Application.Contracts;
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
                if (vendedores == null) return NotFound("Vendedores não encontrados.");

                return Ok(vendedores);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }
    }
}