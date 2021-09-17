using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GECORO.Application.Contracts;
using GECORO.Application.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GECORO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContratoController : ControllerBase
    {
        private readonly IContratoService contratoService;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ContratoController(IContratoService contratoService,
                                IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
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

        [HttpPost("upload-planilha")]
        public IActionResult Upload()
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    if (file.Length > 0)
                    {
                        string path = SaveFile(file).Result;
                        if (contratoService.ProcessaContratoViaPlanilha(path).Result)
                        {
                            if (System.IO.File.Exists(path))
                                System.IO.File.Delete(path);

                            return Ok();
                        }
                    }
                }
                return BadRequest("Não foi possível processar os contratos da planilha.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar processar contratos. Erro: {ex.Message}");
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
                            Ok(new { message = "Deletado" }) :
                            BadRequest("Contrato não deletado.");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar deletar contrato. Erro: {ex.Message}");
                throw;
            }
        }

        [NonAction]
        public async Task<string> SaveFile(IFormFile file)
        {
            string fileName = new String(Path.GetFileNameWithoutExtension(file.FileName)
                                            .Take(10)
                                            .ToArray()
                                            ).Replace(' ', '-');

            fileName = $"{fileName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(file.FileName)}";

            var filePath = Path.Combine(webHostEnvironment.ContentRootPath, @"Resources/", fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}