using Microsoft.EntityFrameworkCore;
using Teste_Taking_Jsl.Data.Contexts;
using Teste_Taking_Jsl.Domain.Interfaces;
using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Data.Repository
{
    public class ClienteRepository: IClienteRepository
    {
        private readonly TakingJslContext _context;

        public ClienteRepository(TakingJslContext context)
        {
            _context = context;
        }

        public async Task SalvarClienteAsync(Cliente cliente)
        {
            await _context.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }
        public void AtualizarCliente(Cliente cliente)
        {
            _context.Update(cliente);
            _context.SaveChanges();
        }

        public void DeletarCliente(Cliente cliente)
        {
            _context.RemoveRange(cliente);
            _context.SaveChanges();
        }

        public async Task<List<Cliente>> ListarClienteAsync() => await _context.Cliente.ToListAsync();

        public async Task<Cliente> ObterClienteAsync(string email) =>
           await _context.Cliente.FirstOrDefaultAsync(p => p.Email.Equals(email));

        public async Task<Cliente> ObterClienteAsync(int id) =>
           await _context.Cliente.FirstOrDefaultAsync(p => p.Id.Equals(id));
    }
}
