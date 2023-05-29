using Microsoft.EntityFrameworkCore;
using Teste_Taking_Jsl.Data.Mappings;
using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Data.Contexts
{
    public class TakingJslContext : DbContext
    {
        public TakingJslContext(DbContextOptions<TakingJslContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new PedidoMap());
        }
    }
}

