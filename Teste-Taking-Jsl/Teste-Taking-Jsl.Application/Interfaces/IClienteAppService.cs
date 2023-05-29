using Teste_Taking_Jsl.Application.Models;
using Teste_Taking_Jsl.Common;

namespace Teste_Taking_Jsl.Application.Interfaces
{
    public interface IClienteAppService
    {
        Task<Resultado<ClienteViewModel>> SalvarClienteAsync(ClienteViewModel cliente);
        Task<Resultado<ClienteViewModel>> AtualizarClienteAsync(ClienteViewModel cliente);
        Task<Resultado<List<ClienteViewModel>>> ListarClienteAsync();
        Task<Resultado<ClienteViewModel>> ObterClienteAsync(int usuarioId);
        Task<Resultado<ClienteViewModel>> DeletarClienteAsync(int id);

        Task<Resultado<List<ClienteViewModel>>> ListarClienteAtivoAsync();
    }
}
