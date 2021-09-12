using System.Linq;
using System.Threading.Tasks;
using GECORO.Domain;
using GECORO.Persistence.Context;
using GECORO.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GECORO.Persistence
{
    public class ParcelaPersist : IParcelaPersist
    {
        private readonly GecoroContext context;
        public ParcelaPersist(GecoroContext context)
        {
            this.context = context;
        }

        public async Task<Parcela> GetParcelaByIdAsync(int id)
        {
            IQueryable<Parcela> query = context.Parcelas;

            query = query.OrderBy(c => c.Id).Where(p => p.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Parcela[]> GetAllParcelasByContratoAsync(int contratoId)
        {
            IQueryable<Parcela> query = context.Parcelas;

            query = query.OrderBy(c => c.Id).Where(p => p.ContratoId == contratoId);

            return await query.ToArrayAsync();
        }
    }
}