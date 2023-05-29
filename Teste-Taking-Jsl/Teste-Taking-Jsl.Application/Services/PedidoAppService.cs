using AutoMapper;
using Teste_Taking_Jsl.Application.Interfaces;
using Teste_Taking_Jsl.Application.Models;
using Teste_Taking_Jsl.Common;
using Teste_Taking_Jsl.Domain.Interfaces;
using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Application.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public PedidoAppService(IPedidoRepository pedidoRepository, IMapper mapper, IProdutoRepository produtoRepository, IClienteRepository clienteRepository)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
        }
        public async Task<Resultado<PedidoViewModel>> SalvarPedidoAsync(PedidoViewModel pedidoViewModel)
        {
            try
            {
                var valorTotal = await CalcularValorTotalAsync(pedidoViewModel.Quantidade, pedidoViewModel.ProdutoId);
                var produto = await AjusteProdutoEstoqueAsync(pedidoViewModel.Quantidade, pedidoViewModel.ProdutoId);
                if (produto == null)
                {
                    return Resultado<PedidoViewModel>.InformacaoMensagem("Produto indisponivel");
                }

                var pedido = _mapper.Map<Pedido>(pedidoViewModel);
                pedido.DataPedido = DateTime.Now.Date;
                pedido.DataAlteracao = DateTime.Now.Date;
                pedido.ValorTotal = valorTotal;
                await _pedidoRepository.SalvarPedidoAsync(pedido);
                return Resultado<PedidoViewModel>.SucessoMensagem("Produto cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return Resultado<PedidoViewModel>.ErroMensagem($"Erro ao cadastrar o produto {ex.Message}!");
            }
        }

        public async Task<Resultado<PedidoViewModel>> AtualizarPedidoAsync(PedidoViewModel pedidoViewModel)
        {
            try
            {
                var pedidoEntity = await _pedidoRepository.ObterPedidoAsync(pedidoViewModel.Id);
                if (pedidoEntity == null)
                    return Resultado<PedidoViewModel>.InformacaoMensagem("pedido não cadastrado!");

                var valorTotal = await CalcularValorTotalAsync(pedidoViewModel.Quantidade, pedidoViewModel.ProdutoId);
                var produto = await AjusteProdutoEstoqueAsync(pedidoViewModel.Quantidade, pedidoViewModel.ProdutoId);
                if (produto == null)
                {
                    return Resultado<PedidoViewModel>.InformacaoMensagem("Produto indisponivel");
                }
                pedidoEntity = _mapper.Map<Pedido>(pedidoViewModel);
                pedidoEntity.DataAlteracao = DateTime.Now.Date;
                pedidoEntity.ValorTotal = valorTotal;
                _pedidoRepository.AtualizarPedido(pedidoEntity);
                return Resultado<PedidoViewModel>.SucessoMensagem("produto atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return Resultado<PedidoViewModel>.ErroMensagem($"Erro ao atualizar produto {ex.Message}!");
            }
        }

        public async Task<Resultado<PedidoViewModel>> ObterPedidoAsync(int id)
        {
            try
            {
                var pedido = await _pedidoRepository.ObterPedidoAsync(id);
                var produtoViewModel = _mapper.Map<PedidoViewModel>(pedido);
                var resultado = new Resultado<PedidoViewModel>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = produtoViewModel
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return Resultado<PedidoViewModel>.ErroMensagem($"Erro ao obter o pedido {ex.Message}!");
            }
        }
       
        public async Task<Resultado<List<PedidoViewModel>>> ListarPedidoAsync()
        {
            try
            {
                var lstPedido = await _pedidoRepository.ListarPedidoAsync();
                var lstPedidoViewModel = _mapper.Map<List<PedidoViewModel>>(lstPedido);
                foreach (var item in lstPedidoViewModel)
                {
                    var produto = await _produtoRepository.ObterProdutoAsync(item.ProdutoId);
                    var cliente = await _clienteRepository.ObterClienteAsync(item.ClienteId);
                    item.Produto = produto.Nome;
                    item.Cliente = cliente.Nome;
                }
                var resultado = new Resultado<List<PedidoViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = lstPedidoViewModel
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<List<PedidoViewModel>>()
                {
                    Mensagem = $"Erro ao listar os pedidos: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
        }

        public async Task<Resultado<PedidoViewModel>> DeletarPedidoAsync(int id)
        {
            try
            {
                var pedidoEntity = await _pedidoRepository.ObterPedidoAsync(id);
                if (pedidoEntity == null)
                    return Resultado<PedidoViewModel>.InformacaoMensagem("Pedido não cadastrado!");

                _pedidoRepository.DeletarPedido(pedidoEntity);
                return Resultado<PedidoViewModel>.SucessoMensagem("Pedido Excluido Com Sucesso");
            }
            catch (Exception ex)
            {
                return Resultado<PedidoViewModel>.ErroMensagem($"Erro ao exluir Pedido {ex.Message}!");
            }
        }
        private async Task<decimal> CalcularValorTotalAsync(int qtd, int idProduto)
        {
            var produto = await _produtoRepository.ObterProdutoAsync(idProduto);
            var valorTotal = produto.Preco * qtd;
            return valorTotal;
        }

        private async Task<Produto> AjusteProdutoEstoqueAsync(int qtd, int idProduto)
        {
            var produto = await _produtoRepository.ObterProdutoAsync(idProduto);
            produto.Quantidade -= qtd;
            if (produto.Quantidade.Equals((int)default))
            {
                produto.Status = false;
                await _produtoRepository.SalvarProdutoAsync(produto);
            }
            return produto;
        }
    }
}
