using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task SalvarClienteAsync(Cliente cliente);
        Task<Cliente> ObterClienteAsync(string email);
        Task<Cliente> ObterClienteAsync(int id);
        Task<List<Cliente>> ListarClienteAsync();
        Task<List<Cliente>> ListarClienteAtivosAsync();
        void AtualizarCliente(Cliente cliente);
        void DeletarCliente(Cliente cliente);

    }
}
