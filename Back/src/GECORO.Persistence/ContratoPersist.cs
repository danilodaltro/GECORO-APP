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
        }
        public async Task<Contrato[]> GetAllContratosAsync()
        {
            IQueryable<Contrato> query = context.Contratos.Include(ctr => ctr.Parcelas);

            query = query.OrderBy(ctr => ctr.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Contrato[]> GetAllContratosByClienteAsync(int clienteId)
        {
            IQueryable<Contrato> query = context.Contratos.Include(ctr => ctr.Parcelas);

            query = query.OrderBy(ctr => ctr.Id).Where(ctr => ctr.ClienteId == clienteId);

            return await query.ToArrayAsync();
        }

        public async Task<Contrato> GetContratoByNumeroAsync(string numeroContrato)
        {
            IQueryable<Contrato> query = context.Contratos.Include(ctr => ctr.Parcelas);

            query = query.OrderBy(ctr => ctr.Id).Where(ctr => ctr.NuContrato == numeroContrato);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Contrato> GetContratoByIdAsync(int contratoId)
        {
            IQueryable<Contrato> query = context.Contratos.Include(ctr => ctr.Parcelas);

            query = query.OrderBy(ctr => ctr.Id).Where(ctr => ctr.Id == contratoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}