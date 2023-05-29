using Teste_Taking_Jsl.Application.Models;
using Teste_Taking_Jsl.Common;

namespace Teste_Taking_Jsl.Application.Interfaces
{
    public interface IPedidoAppService
    {
        Task<Resultado<PedidoViewModel>> SalvarPedidoAsync(PedidoViewModel pedidoViewModel);
        Task<Resultado<PedidoViewModel>> AtualizarPedidoAsync(PedidoViewModel pedidoViewModel);
        Task<Resultado<PedidoViewModel>> ObterPedidoAsync(int id);
        Task<Resultado<List<PedidoViewModel>>> ListarPedidoAsync();
        Task<Resultado<PedidoViewModel>> DeletarPedidoAsync(int id);
    }
}
