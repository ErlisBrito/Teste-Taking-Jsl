using Teste_Taking_Jsl.Application.Models;
using Teste_Taking_Jsl.Common;

namespace Teste_Taking_Jsl.Application.Interfaces
{
    public interface IProdutoAppService
    {
        Task<Resultado<ProdutoViewModel>> SalvarProdutoAsync(ProdutoViewModel produtoViewModel);
        Task<Resultado<ProdutoViewModel>> AtualizarProdutoAsync(ProdutoViewModel produtoViewModel);
        Task<Resultado<ProdutoViewModel>> DeletarProdutoAsync(int id);
        Task<Resultado<List<ProdutoViewModel>>> ListarProdutoAsync();
        Task<Resultado<ProdutoViewModel>> ObterClienteAsync(int id);
        Task<Resultado<List<ProdutoViewModel>>> ListarProdutoAtivoAsync();
    }
}
