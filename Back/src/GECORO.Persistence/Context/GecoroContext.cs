using GECORO.Domain;
using Microsoft.EntityFrameworkCore;

namespace GECORO.Persistence.Context
{
    public class GecoroContext: DbContext
    {
        public GecoroContext(DbContextOptions<GecoroContext> options): base(options)
        {
            
        }

        public DbSet<Vendedor> Vendedores {get; set;}
        public DbSet<Cliente> Clientes {get; set;}
        public DbSet<Contrato> Contratos {get; set;}
        public DbSet<VendedorCliente> VendedoresClientes {get; set;}
        public DbSet<Parcela> Parcelas {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendedorCliente>().HasKey(VE => new {VE.VendedorId, VE.ClienteId});

            modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Contratos)
            .WithOne(ct => ct.Cliente)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}