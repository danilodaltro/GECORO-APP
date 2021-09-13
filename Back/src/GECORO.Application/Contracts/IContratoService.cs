using System.Threading.Tasks;
using GECORO.Application.Dto;
using GECORO.Domain;

namespace GECORO.Application.Contracts
{
    public interface IContratoService
    {
        public Task<ContratoDto> AddContrato(ContratoDto model);
        public Task<ContratoDto> UpdateContrato(int contratoId, ContratoDto model);
        public Task<bool> DeleteContrato(int contratoId);
        Task<ContratoDto[]> GetAllContratosAsync();
        Task<ContratoDto[]> GetAllContratosByClienteAsync(int clienteId);
        Task<ContratoDto[]> GetAllContratosByVendedorAsync(int vendedorId);
        Task<ContratoDto> GetContratoByIdAsync(int contratoId);
    }
}