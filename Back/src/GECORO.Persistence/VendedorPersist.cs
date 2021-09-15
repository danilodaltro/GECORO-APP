using System.Linq;
using System.Threading.Tasks;
using GECORO.Domain;
using GECORO.Persistence.Context;
using GECORO.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GECORO.Persistence
{
    public class VendedorPersist : IVendedorPersist
    {
        private readonly GecoroContext context;
        public VendedorPersist(GecoroContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Vendedor[]> GetAllVendedoresAsync(bool incluiClientes = false)
        {
            IQueryable<Vendedor> query = context.Vendedores
                                        .Include(v => v.RegraVendedor);

            if (incluiClientes)
                query.Include(v => v.Clientes);

            query = query.OrderBy(v => v.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Vendedor[]> GetAllVendedoresByNomeAsync(string nome, bool incluiClientes)
        {
            IQueryable<Vendedor> query = context.Vendedores
                            .Include(v => v.RegraVendedor);

            if (incluiClientes)
                query.Include(v => v.Clientes);

            query = query.OrderBy(v => v.Id).Where(v => v.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Vendedor> GetVendedorByCodigoAsync(string codigo, bool incluiClientes = false)
        {
            IQueryable<Vendedor> query = context.Vendedores
                            .Include(v => v.RegraVendedor);

            if (incluiClientes)
                query.Include(v => v.Clientes);

            query = query.OrderBy(v => v.Id).Where(v => v.Codigo == codigo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Vendedor> GetVendedorByIdAsync(int id, bool incluiClientes = false)
        {
            IQueryable<Vendedor> query = context.Vendedores
                .Include(v => v.RegraVendedor);

            if (incluiClientes)
                query.Include(v => v.Clientes);

            query = query.OrderBy(v => v.Id).Where(v => v.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        
        public async Task<Vendedor> GetVendedorByRegraAsync(int parcelasPagas,
                                                            decimal saldoDevedor,
                                                            string cpfCliente)
        {
            IQueryable<Vendedor> query = context.Vendedores
                                                .Include(v => v.RegraVendedor)
                                                .Include(v => v.Clientes);

            query = query.OrderBy(c => c.Id).Where(v => (v.RegraVendedor.ParcelasPagas >= parcelasPagas
                                                    && v.RegraVendedor.SaldoDevedor >= saldoDevedor)
                                                    && v.Clientes.Any(c => c.CPF == cpfCliente));
                                                    

            return await query.FirstOrDefaultAsync();
        }

    }
}