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
                                        .Include(v => v.RegraContrato);

            if (incluiClientes)
                query.Include(v => v.VendedoresClientes).ThenInclude(c => c.Cliente);

            query = query.OrderBy(v => v.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Vendedor[]> GetAllVendedoresByNomeAsync(string nome, bool incluiClientes)
        {
            IQueryable<Vendedor> query = context.Vendedores
                            .Include(v => v.RegraContrato);

            if (incluiClientes)
                query.Include(v => v.VendedoresClientes).ThenInclude(c => c.Cliente);

            query = query.OrderBy(v => v.Id).Where(v => v.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Vendedor> GetVendedorByCodigoAsync(string codigo, bool incluiClientes = false)
        {
            IQueryable<Vendedor> query = context.Vendedores
                            .Include(v => v.RegraContrato);

            if (incluiClientes)
                query.Include(v => v.VendedoresClientes).ThenInclude(c => c.Cliente);

            query = query.OrderBy(v => v.Id).Where(v => v.Codigo == codigo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Vendedor> GetVendedorByIdAsync(int id, bool incluiClientes = false)
        {
            IQueryable<Vendedor> query = context.Vendedores
                .Include(v => v.RegraContrato);

            if (incluiClientes)
                query.Include(v => v.VendedoresClientes).ThenInclude(c => c.Cliente);

            query = query.OrderBy(v => v.Id).Where(v => v.Id == id);

            return await query.FirstOrDefaultAsync();
        }

    }
}