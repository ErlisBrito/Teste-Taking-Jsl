using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Data.Mappings
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Cliente)
                   .WithMany(p => p.Pedido)
                   .HasForeignKey(p => p.ClienteId);

            builder.HasOne(p => p.Produto)
                   .WithMany(p => p.Pedido)
                   .HasForeignKey(p => p.ProdutoId);

        }
    }
}
