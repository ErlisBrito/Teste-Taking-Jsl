using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task SalvarPedidoAsync(Pedido pedido);
        void AtualizarPedido(Pedido pedido);
        void DeletarPedido(Pedido pedido);

        Task<Pedido> ObterPedidoAsync(int id);
        Task<List<Pedido>> ListarPedidoAsync();
    }
}
