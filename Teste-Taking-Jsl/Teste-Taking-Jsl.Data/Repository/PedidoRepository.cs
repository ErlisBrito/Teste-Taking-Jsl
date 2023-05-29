using Microsoft.EntityFrameworkCore;
using Teste_Taking_Jsl.Data.Contexts;
using Teste_Taking_Jsl.Domain.Interfaces;
using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Data.Repository
{
    public class PedidoRepository: IPedidoRepository
    {
        private readonly TakingJslContext _context;
        
        public PedidoRepository(TakingJslContext context)
        {
            _context = context; 
        }

        public async Task SalvarPedidoAsync(Pedido pedido)
        {
            await _context.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public void AtualizarPedido(Pedido pedido)
        {
            _context.Update(pedido);
            _context.SaveChanges();
        }

        public void DeletarPedido(Pedido pedido)
        {
            _context.RemoveRange(pedido);
            _context.SaveChanges();
        }


        public async Task<Pedido> ObterPedidoAsync(int id) =>
               await _context.Pedido.FirstOrDefaultAsync(p => p.Id.Equals(id));
        public async Task<List<Pedido>> ListarPedidoAsync() => await _context.Pedido.ToListAsync();
    }
}
