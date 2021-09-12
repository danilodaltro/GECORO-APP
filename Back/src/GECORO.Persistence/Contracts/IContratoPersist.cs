using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Persistence.Contracts
{
    public interface IContratoPersist
    {
        Task<Contrato[]> GetAllContratosAsync();
        Task<Contrato[]> GetAllContratosByClienteAsync(int clienteId);
        Task<Contrato> GetContratoByNumeroAsync(string numeroContrato);
        Task<Contrato> GetContratoByIdAsync(int contratoId);
    }
}