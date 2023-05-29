using AutoMapper;
using Teste_Taking_Jsl.Application.Interfaces;
using Teste_Taking_Jsl.Application.Models;
using Teste_Taking_Jsl.Common;
using Teste_Taking_Jsl.Domain.Interfaces;
using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Application.Services
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;

        private readonly IMapper _mapper;

        public ClienteAppService(IMapper mapper, IClienteRepository clienteRepository)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        public async Task<Resultado<ClienteViewModel>> SalvarClienteAsync(ClienteViewModel clienteViewModel)
        {
            try
            {
                var cliente = await _clienteRepository.ObterClienteAsync(clienteViewModel.Email);
                if (cliente != null)
                    return Resultado<ClienteViewModel>.InformacaoMensagem("E-mail do Cliente já cadastrado!");

                cliente = _mapper.Map<Cliente>(clienteViewModel);
                cliente.DataCadastro = DateTime.Now;
                cliente.DataAlteracao = DateTime.Now;
                await _clienteRepository.SalvarClienteAsync(cliente);
                return Resultado<ClienteViewModel>.SucessoMensagem("Cliente cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return Resultado<ClienteViewModel>.ErroMensagem($"Erro ao cadastrar o Cliente {ex.Message}!");
            }
        }
        public async Task<Resultado<ClienteViewModel>> AtualizarClienteAsync(ClienteViewModel clienteViewModel)
        {
            try
            {
                var clienteEntity = await _clienteRepository.ObterClienteAsync(clienteViewModel.Id);
                if (clienteEntity == null)
                    return Resultado<ClienteViewModel>.InformacaoMensagem("Cliente não cadastrado!");

                var Cliente = clienteEntity;
                clienteEntity = _mapper.Map<Cliente>(clienteViewModel);
                clienteEntity.DataAlteracao = DateTime.Now;
                clienteEntity.DataCadastro = Cliente.DataCadastro;
                _clienteRepository.AtualizarCliente(clienteEntity);
                return Resultado<ClienteViewModel>.SucessoMensagem("Cliente atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return Resultado<ClienteViewModel>.ErroMensagem($"Erro ao atualizar o Cliente {ex.Message}!");
            }
        }

        public async Task<Resultado<ClienteViewModel>> DeletarClienteAsync(int id)
        {
            try
            {
                var clienteEntity = await _clienteRepository.ObterClienteAsync(id);
                if (clienteEntity == null)
                    return Resultado<ClienteViewModel>.InformacaoMensagem("Cliente não cadastrado!");
                _clienteRepository.DeletarCliente(clienteEntity);
                return Resultado<ClienteViewModel>.SucessoMensagem("Cliente Excluido Com Sucesso com sucesso!");
            }
            catch (Exception ex)
            {
                return Resultado<ClienteViewModel>.ErroMensagem($"Erro ao exluir Cliente {ex.Message}!");
            }
        }

        public async Task<Resultado<List<ClienteViewModel>>> ListarClienteAsync()
        {
            try
            {
                var lstCliente = await _clienteRepository.ListarClienteAsync();
                var lstClienteViewModel = _mapper.Map<List<ClienteViewModel>>(lstCliente);
                var resultado = new Resultado<List<ClienteViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = lstClienteViewModel
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<List<ClienteViewModel>>()
                {
                    Mensagem = $"Erro ao listar os cliente: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
        }

        public async Task<Resultado<List<ClienteViewModel>>> ListarClienteAtivoAsync()
        {
            try
            {
                var lstCliente = await _clienteRepository.ListarClienteAtivosAsync();
                var lstClienteViewModel = _mapper.Map<List<ClienteViewModel>>(lstCliente);
                var resultado = new Resultado<List<ClienteViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = lstClienteViewModel
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<List<ClienteViewModel>>()
                {
                    Mensagem = $"Erro ao listar os cliente: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
        }

        public async Task<Resultado<ClienteViewModel>> ObterClienteAsync(int clienteId)
        {
            try
            {
                var cliente = await _clienteRepository.ObterClienteAsync(clienteId);
                var clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);
                var resultado = new Resultado<ClienteViewModel>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = clienteViewModel
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return Resultado<ClienteViewModel>.ErroMensagem($"Erro ao obter o Cliente {ex.Message}!");
            }
        }
    }
}
