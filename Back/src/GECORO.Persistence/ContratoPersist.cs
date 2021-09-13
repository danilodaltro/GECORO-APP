using System.Linq;
using System.Threading.Tasks;
using GECORO.Domain;
using GECORO.Persistence.Context;
using GECORO.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GECORO.Persistence
{
    public class ContratoPersist : IContratoPersist
    {
        private readonly GecoroContext context;
        public ContratoPersist(GecoroContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public async Task<Contrato[]> GetAllContratosAsync()
        {
            IQueryable<Contrato> query = context.Contratos
                                                .Include(ctr => ctr.Parcelas)
                                                .Include(ctr => ctr.Cliente)
                                                .Include(ctr => ctr.Vendedor);

            query = query.OrderBy(ctr => ctr.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Contrato[]> GetAllContratosByClienteAsync(int clienteId)
        {
            IQueryable<Contrato> query = context.Contratos
                                                .Include(ctr => ctr.Parcelas)
                                                .Include(ctr => ctr.Cliente)
                                                .Include(ctr => ctr.Vendedor);

            query = query.OrderBy(ctr => ctr.Id).Where(ctr => ctr.ClienteId == clienteId);

            return await query.ToArrayAsync();
        }

        public async Task<Contrato[]> GetAllContratosByVendedorAsync(int vendedorId)
        {
            IQueryable<Contrato> query = context.Contratos
                                                .Include(ctr => ctr.Parcelas)
                                                .Include(ctr => ctr.Cliente)
                                                .Include(ctr => ctr.Vendedor);

            query = query.OrderBy(ctr => ctr.Id).Where(ctr => ctr.VendedorId == vendedorId);

            return await query.ToArrayAsync();
        }

        public async Task<Contrato> GetContratoByIdAsync(int contratoId)
        {
            IQueryable<Contrato> query = context.Contratos
                                                .Include(ctr => ctr.Parcelas)
                                                .Include(ctr => ctr.Cliente)
                                                .Include(ctr => ctr.Vendedor);

            query = query.OrderBy(ctr => ctr.Id).Where(ctr => ctr.Id == contratoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}