using System;
using System.Threading.Tasks;
using GECORO.Application.Contracts;
using GECORO.Domain;
using GECORO.Persistence.Contracts;

namespace GECORO.Application
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorPersist vendedorPersist;
        private readonly IGeneralPersist generalPersist;
        public VendedorService(IGeneralPersist generalPersist, IVendedorPersist vendedorPersist)
        {
            this.generalPersist = generalPersist;
            this.vendedorPersist = vendedorPersist;
        }

        public async Task<Vendedor> AddVendedor(Vendedor model)
        {
            try
            {
                generalPersist.Add<Vendedor>(model);
                if (await generalPersist.SaveChangesAsync())
                {
                    return await vendedorPersist.GetVendedorByIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vendedor> UpdateVendedor(int vendedorId, Vendedor model)
        {
            try
            {
                var vendedor = await vendedorPersist.GetVendedorByIdAsync(vendedorId, false);
                if (vendedor == null) return null;

                generalPersist.Update<Vendedor>(model);

                if (await generalPersist.SaveChangesAsync())
                {
                    return await vendedorPersist.GetVendedorByIdAsync(model.Id, false);
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

        public async Task<Vendedor[]> GetAllVendedoresAsync(bool incluiVendedor)
        {
            try
            {
                var vendedores = await vendedorPersist.GetAllVendedoresAsync(incluiVendedor);
                if (vendedores == null) return null;

                return vendedores;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vendedor[]> GetAllVendedoresByNomeAsync(string nome, bool incluiVendedor)
        {
            try
            {
                var vendedores = await vendedorPersist.GetAllVendedoresByNomeAsync(nome, incluiVendedor);
                if (vendedores == null) return null;

                return vendedores;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<Vendedor> GetVendedorByCodigoAsync(string codigo, bool incluiVendedor)
        {
            try
            {
                var vendedor = await vendedorPersist.GetVendedorByCodigoAsync(codigo, incluiVendedor);
                if (vendedor == null) return null;

                return vendedor;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vendedor> GetVendedorByIdAsync(int vendedorId, bool incluiVendedor)
        {
            try
            {
                var vendedor = await vendedorPersist.GetVendedorByIdAsync(vendedorId, incluiVendedor);
                if (vendedor == null) return null;

                return vendedor;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}