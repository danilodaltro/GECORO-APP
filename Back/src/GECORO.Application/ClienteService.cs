using System.Threading.Tasks;
using GECORO.Application.Contracts;
using GECORO.Domain;

namespace GECORO.Application
{
    public class ClienteService : IClienteService
    {
        public Task<Cliente> AddCliente(Cliente model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteCliente(int clienteId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Cliente[]> GetAllClientesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Cliente[]> GetAllClientesByNomeAsync(string nome)
        {
            throw new System.NotImplementedException();
        }

        public Task<Cliente> GetClienteByCPFAsync(string CPF)
        {
            throw new System.NotImplementedException();
        }

        public Task<Cliente> GetClienteByIdAsync(int clienteId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Cliente> UpdateCliente(int clienteId, Cliente model)
        {
            throw new System.NotImplementedException();
        }
    }
}