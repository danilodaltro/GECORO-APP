using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggered;
using GECORO.Domain;
using GECORO.Persistence.Context;

namespace GECORO.Persistence.Trigger
{
    public class ValidarVendedorCliente : IBeforeSaveTrigger<VendedorCliente>
    {
        private readonly GecoroContext context;
        public ValidarVendedorCliente(GecoroContext context)
        {
            this.context = context;
        }
        public Task BeforeSave(ITriggerContext<VendedorCliente> context, CancellationToken cancellationToken)
        {
            if (context.ChangeType == ChangeType.Added)              
            {
                IQueryable<VendedorCliente> query = this.context.VendedoresClientes.AsQueryable();
                query = query.Where(vd => vd.ClienteId == context.Entity.ClienteId);
                if(query.Count() > 0)
                    return Task.FromCanceled(cancellationToken);
            }
            return Task.CompletedTask;
        }
    }
}