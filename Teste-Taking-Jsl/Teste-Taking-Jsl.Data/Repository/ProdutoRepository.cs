using Microsoft.EntityFrameworkCore;
using Teste_Taking_Jsl.Data.Contexts;
using Teste_Taking_Jsl.Domain.Interfaces;
using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Data.Repository
{
    public class ProdutoRepository: IProdutoRepository
    {
        private readonly TakingJslContext _context;

        public ProdutoRepository(TakingJslContext context)
        {
            _context = context;
        }

        public async Task SalvarProdutoAsync(Produto produto)
        {
            await _context.AddAsync(produto);
            await _context.SaveChangesAsync();
        }
        public void AtualizarProduto(Produto produto)
        {
            _context.Update(produto);
            _context.SaveChanges();
        }

        public void DeletarProduto(Produto produto)
        {
            _context.RemoveRange(produto);
            _context.SaveChanges();
        }

        public async Task<List<Produto>> ListarProdutoAsync() => await _context.Produto.ToListAsync();
        public async Task<List<Produto>> ListarProdutoAtivosAsync() => await _context.Produto.Where(p => p.Status).ToListAsync();

        public async Task<Produto> ObterProdutoAsync(string nome) =>
               await _context.Produto.FirstOrDefaultAsync(p => p.Nome.Equals(nome));

        public async Task<Produto> ObterProdutoAsync(int id) =>
               await _context.Produto.FirstOrDefaultAsync(p => p.Id.Equals(id));
    }
}
