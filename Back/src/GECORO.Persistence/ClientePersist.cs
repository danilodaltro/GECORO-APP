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

        public async Task<Cliente[]> GetAllClientesByVendedor(int vendedorId)
        {
            IQueryable<Cliente> query = context.Clientes
                                    .Include(c => c.Contratos)
                                    .Include(vc => vc.Vendedor);

            query = query.OrderBy(c => c.Id)
                         .Where(c => c.VendedorId == vendedorId);

            return await query.ToArrayAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            IQueryable<Cliente> query = context.Clientes
                                        .Include(c => c.Contratos)
                                        .Include(vc => vc.Vendedor);

            query = query.OrderBy(c => c.Id).Where(c => c.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetClienteByCPFAsync(string cpf)
        {
            IQueryable<Cliente> query = context.Clientes
                                        .Include(c => c.Contratos)
                                        .Include(vc => vc.Vendedor);

            query = query.OrderBy(c => c.Id).Where(c => c.CPF == cpf);

            return await query.FirstOrDefaultAsync();
        }
    }
}