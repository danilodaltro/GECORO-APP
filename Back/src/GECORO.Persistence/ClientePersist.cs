using System.Linq;
using System.Threading.Tasks;
using GECORO.Domain;
using GECORO.Persistence.Context;
using GECORO.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GECORO.Persistence
{
    public class ClientePersist : IClientePersist
    {
        private readonly GecoroContext context;
        public ClientePersist(GecoroContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Cliente[]> GetAllClientesAsync()
        {
            IQueryable<Cliente> query = context.Clientes
                                        .Include(c => c.Contratos)
                                        .Include(vc => vc.Vendedor);

            query = query.OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Cliente> GetClienteByCPFAsync(string CPF)
        {
            IQueryable<Cliente> query = context.Clientes
                                        .Include(c => c.Contratos)
                                        .Include(vc => vc.Vendedor);

            query = query.OrderBy(c => c.Id).Where(c => c.CPF == CPF);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            IQueryable<Cliente> query = context.Clientes
                                        .Include(c => c.Contratos)
                                        .Include(vc => vc.Vendedor);

            query = query.OrderBy(c => c.Id).Where(c => c.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cliente[]> GetClientesByNomeAsync(string nome)
        {
            IQueryable<Cliente> query = context.Clientes
                                                .Include(c => c.Contratos)
                                                .Include(vc => vc.Vendedor);

            query = query.OrderBy(c => c.Id).Where(c => c.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}