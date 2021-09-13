using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Persistence.Contracts
{
    public interface IContratoPersist
    {
        Task<Contrato[]> GetAllContratosAsync();
        Task<Contrato[]> GetAllContratosByClienteAsync(int clienteId);
        Task<Contrato[]> GetAllContratosByVendedorAsync(int vendedorId);
        Task<Contrato> GetContratoByIdAsync(int contratoId);
    }
}