using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Application.Contracts
{
    public interface IClienteService
    {
        public Task<Cliente> AddCliente(Cliente model);
        public Task<Cliente> UpdateCliente(int clienteId, Cliente model);
        public Task<bool> DeleteCliente(int clienteId); 
        Task<Cliente[]> GetAllClientesAsync();
        Task<Cliente[]> GetAllClientesByNomeAsync(string nome);
        Task<Cliente> GetClienteByCPFAsync(string CPF);
        Task<Cliente> GetClienteByIdAsync(int clienteId);
    }
}