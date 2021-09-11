using System.Threading.Tasks;
using GECORO.Persistence.Context;
using GECORO.Persistence.Contracts;

namespace GECORO.Persistence
{
    public class GeneralPersist : IGeneralPersist
    {
        private readonly GecoroContext context;
        public GeneralPersist(GecoroContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public void DeleteRange<T>(T entityArray) where T : class
        {
            context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}