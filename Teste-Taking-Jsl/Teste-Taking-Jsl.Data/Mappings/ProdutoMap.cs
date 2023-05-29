using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Data.Mappings
{
    public class ProdutoMap:IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
