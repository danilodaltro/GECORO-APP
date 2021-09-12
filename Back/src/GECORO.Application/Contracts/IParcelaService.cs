using System.Threading.Tasks;
using GECORO.Application.Dto;

namespace GECORO.Application.Contracts
{
    public interface IParcelaService
    {
        public Task<ParcelaDto> AddParcela(ParcelaDto model);
        public Task<ParcelaDto> UpdateParcela(int parcelaId, ParcelaDto model);
        public Task<bool> DeleteParcela(int parcelaId);
        Task<ParcelaDto[]> GetAllParcelasByContratoAsync(int contratoId);
        Task<ParcelaDto> GetParcelaById(int parcelaId);
    }
}