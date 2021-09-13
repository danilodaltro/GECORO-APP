using System.Threading.Tasks;
using GECORO.Application.Dto;
using GECORO.Domain;

namespace GECORO.Application.Contracts
{
    public interface IVendedorService
    {
        public Task<VendedorDto> AddVendedor(VendedorDto model);
        public Task<VendedorDto> UpdateVendedor(int vendedorId, VendedorDto model);
        public Task<bool> DeleteVendedor(int vendedorId);
        Task<VendedorDto[]> GetAllVendedoresAsync(bool incluiClientes);
        Task<VendedorDto[]> GetAllVendedoresByNomeAsync(string nome, bool incluiClientes);
        Task<VendedorDto> GetVendedorByCodigoAsync(string codigo, bool incluiClientes);
        Task<VendedorDto> GetVendedorByIdAsync(int vendedorId, bool incluiClientes);
        Task<VendedorDto> GetVendedorByRegraAsync(int parcelasPagas, decimal saldoDevedor);
    }

}