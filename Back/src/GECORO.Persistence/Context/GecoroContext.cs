using GECORO.Domain;
using Microsoft.EntityFrameworkCore;

namespace GECORO.Persistence.Context
{
    public class GecoroContext : DbContext
    {
        public GecoroContext(DbContextOptions<GecoroContext> options) : base(options)
        {

        }

        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Vendedor>()
            .HasAlternateKey(v => v.Codigo);

            modelBuilder.Entity<Contrato>()
            .HasAlternateKey(ct => ct.NuContrato);

            modelBuilder.Entity<Cliente>()
            .HasAlternateKey(c => c.CPF);

            modelBuilder.Entity<Vendedor>()
            .HasMany(v => v.Clientes)
            .WithOne(c => c.Vendedor)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Vendedor>()
            .HasOne(v => v.RegraVendedor)
            .WithOne(rv => rv.Vendedor)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Contratos)
            .WithOne(ct => ct.Cliente)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contrato>()
            .HasMany(ct => ct.Parcelas)
            .WithOne(p => p.Contrato)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Contrato>()
            .HasOne(ct => ct.Cliente)
            .WithMany(c => c.Contratos)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Parcela>()
            .HasOne(p => p.Contrato)
            .WithMany(ct => ct.Parcelas)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}