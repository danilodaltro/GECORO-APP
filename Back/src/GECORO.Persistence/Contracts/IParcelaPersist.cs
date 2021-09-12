using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Persistence.Contracts
{
    public interface IParcelaPersist
    {
        Task<Parcela[]> GetAllParcelasByContratoAsync(int contratoId);
        Task<Parcela> GetParcelaByIdAsync(int id);
    }
}