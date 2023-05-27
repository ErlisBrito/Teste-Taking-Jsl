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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
        }
    }
}

