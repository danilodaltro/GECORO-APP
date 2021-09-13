using System;
using System.Threading.Tasks;
using AutoMapper;
using GECORO.Application.Contracts;
using GECORO.Application.Dto;
using GECORO.Domain;
using GECORO.Persistence.Contracts;

namespace GECORO.Application
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorPersist vendedorPersist;
        private readonly IGeneralPersist generalPersist;
        private readonly IMapper mapper;
        public VendedorService(IGeneralPersist generalPersist, IVendedorPersist vendedorPersist, IMapper mapper)
        {
            this.vendedorPersist = vendedorPersist;
            this.generalPersist = generalPersist;
            this.mapper = mapper;
        }

        public async Task<VendedorDto> AddVendedor(VendedorDto model)
        {
            try
            {
                var vendedor = mapper.Map<Vendedor>(model);

                var validaVendedor = await vendedorPersist.GetVendedorByCodigoAsync(vendedor.Codigo);
                if (validaVendedor != null)
                    return null;

                generalPersist.Add<Vendedor>(vendedor);
                if (await generalPersist.SaveChangesAsync())
                {
                    var vendedorRetorno = await vendedorPersist.GetVendedorByIdAsync(vendedor.Id, false);
                    return mapper.Map<VendedorDto>(vendedorRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VendedorDto> UpdateVendedor(int vendedorId, VendedorDto model)
        {
            try
            {
                var vendedor = await vendedorPersist.GetVendedorByIdAsync(vendedorId, false);
                if (vendedor == null) return null;

                model.Id = vendedor.Id;
                mapper.Map(model, vendedor);

                generalPersist.Update<Vendedor>(vendedor);

                if (await generalPersist.SaveChangesAsync())
                {
                    var vendedorRetorno = await vendedorPersist.GetVendedorByIdAsync(vendedor.Id, false);
                    return mapper.Map<VendedorDto>(vendedorRetorno);
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteVendedor(int vendedorId)
        {
            try
            {
                Vendedor vendedor = await vendedorPersist.GetVendedorByIdAsync(vendedorId, false);
                if (vendedor == null) throw new Exception("O vendedor a ser deletado não foi encontrado.");

                generalPersist.Delete<Vendedor>(vendedor);
                return await generalPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VendedorDto[]> GetAllVendedoresAsync(bool incluiClientes)
        {
            try
            {
                var vendedores = await vendedorPersist.GetAllVendedoresAsync(incluiClientes);
                if (vendedores == null) return null;

                return mapper.Map<VendedorDto[]>(vendedores);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VendedorDto[]> GetAllVendedoresByNomeAsync(string nome, bool incluiClientes)
        {
            try
            {
                var vendedores = await vendedorPersist.GetAllVendedoresByNomeAsync(nome, incluiClientes);
                if (vendedores == null) return null;

                return mapper.Map<VendedorDto[]>(vendedores);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<VendedorDto> GetVendedorByCodigoAsync(string codigo, bool incluiClientes)
        {
            try
            {
                var vendedor = await vendedorPersist.GetVendedorByCodigoAsync(codigo, incluiClientes);
                if (vendedor == null) return null;

                return mapper.Map<VendedorDto>(vendedor);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VendedorDto> GetVendedorByIdAsync(int vendedorId, bool incluiClientes)
        {
            try
            {
                var vendedor = await vendedorPersist.GetVendedorByIdAsync(vendedorId, incluiClientes);
                if (vendedor == null) return null;

                return mapper.Map<VendedorDto>(vendedor);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VendedorDto> GetVendedorByRegraAsync(int parcelasPagas, decimal saldoDevedor)
        {
            try
            {
                var vendedor = await vendedorPersist.GetVendedorByRegraAsync(parcelasPagas, saldoDevedor);
                if (vendedor == null) return null;

                return mapper.Map<VendedorDto>(vendedor);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}