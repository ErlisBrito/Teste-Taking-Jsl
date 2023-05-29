using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task SalvarProdutoAsync(Produto produto);
        void AtualizarProduto(Produto produto);
        void DeletarProduto(Produto produto);
        Task<List<Produto>> ListarProdutoAsync();
        Task<List<Produto>> ListarProdutoAtivosAsync();
        Task<Produto> ObterProdutoAsync(string nome);
        Task<Produto> ObterProdutoAsync(int id);
    }
}
