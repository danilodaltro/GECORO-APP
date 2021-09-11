using GECORO.Application.Contracts;
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
    }
}