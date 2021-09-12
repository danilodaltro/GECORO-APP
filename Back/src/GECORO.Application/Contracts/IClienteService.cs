using System.Threading.Tasks;
using GECORO.Application.Dto;
using GECORO.Domain;

namespace GECORO.Application.Contracts
{
    public interface IClienteService
    {
        public Task<ClienteDto> AddCliente(ClienteDto model);
        public Task<ClienteDto> UpdateCliente(int clienteId, ClienteDto model);
        public Task<bool> DeleteCliente(int clienteId); 
        Task<ClienteDto[]> GetAllClientesAsync();
        Task<ClienteDto[]> GetAllClientesByNomeAsync(string nome);
        Task<ClienteDto> GetClienteByCPFAsync(string CPF);
        Task<ClienteDto> GetClienteByIdAsync(int clienteId);
    }
}