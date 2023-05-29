using AutoMapper;
using Teste_Taking_Jsl.Application.Interfaces;
using Teste_Taking_Jsl.Application.Models;
using Teste_Taking_Jsl.Common;
using Teste_Taking_Jsl.Domain.Interfaces;
using Teste_Taking_Jsl.Domain.Models;

namespace Teste_Taking_Jsl.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public ProdutoAppService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<Resultado<ProdutoViewModel>> SalvarProdutoAsync(ProdutoViewModel produtoViewModel)
        {
            try
            {
                var produto = await _produtoRepository.ObterProdutoAsync(produtoViewModel.Nome);
                if (produto != null)
                    return Resultado<ProdutoViewModel>.InformacaoMensagem("Produto já cadastrado!");

                produto = _mapper.Map<Produto>(produtoViewModel);
                produto.DataCadastro = DateTime.Now;
                produto.DataAlteracao = DateTime.Now;
                await _produtoRepository.SalvarProdutoAsync(produto);
                return Resultado<ProdutoViewModel>.SucessoMensagem("Produto cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return Resultado<ProdutoViewModel>.ErroMensagem($"Erro ao cadastrar o produto {ex.Message}!");
            }
        }

        public async Task<Resultado<ProdutoViewModel>> AtualizarProdutoAsync(ProdutoViewModel produtoViewModel)
        {
            try
            {
                var produtoEntity = await _produtoRepository.ObterProdutoAsync(produtoViewModel.Id);
                if (produtoEntity == null)
                    return Resultado<ProdutoViewModel>.InformacaoMensagem("produto não cadastrado!");

                var produto = _mapper.Map<Produto>(produtoViewModel);
                produto.DataAlteracao = DateTime.Now;
                produto.DataCadastro = produtoEntity.DataCadastro;
                _produtoRepository.AtualizarProduto(produto);
                return Resultado<ProdutoViewModel>.SucessoMensagem("produto atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return Resultado<ProdutoViewModel>.ErroMensagem($"Erro ao atualizar produto {ex.Message}!");
            }
        }

        public async Task<Resultado<ProdutoViewModel>> DeletarProdutoAsync(int id)
        {
            try
            {
                var produtoEntity = await _produtoRepository.ObterProdutoAsync(id);
                if (produtoEntity == null)
                    return Resultado<ProdutoViewModel>.InformacaoMensagem("Produto não cadastrado!");

                _produtoRepository.DeletarProduto(produtoEntity);
                return Resultado<ProdutoViewModel>.SucessoMensagem("Produto Excluido Com Sucesso com sucesso!");
            }
            catch (Exception ex)
            {
                return Resultado<ProdutoViewModel>.ErroMensagem($"Erro ao exluir Produto {ex.Message}!");
            }
        }

        public async Task<Resultado<List<ProdutoViewModel>>> ListarProdutoAsync()
        {
            try
            {
                var lstProduto = await _produtoRepository.ListarProdutoAsync();
                var lstProdutoViewModel = _mapper.Map<List<ProdutoViewModel>>(lstProduto);
                var resultado = new Resultado<List<ProdutoViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = lstProdutoViewModel
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<List<ProdutoViewModel>>()
                {
                    Mensagem = $"Erro ao listar os cliente: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
        }

        public async Task<Resultado<List<ProdutoViewModel>>> ListarProdutoAtivoAsync()
        {
            try
            {
                var lstProduto = await _produtoRepository.ListarProdutoAtivosAsync();
                var lstProdutoViewModel = _mapper.Map<List<ProdutoViewModel>>(lstProduto);
                var resultado = new Resultado<List<ProdutoViewModel>>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = lstProdutoViewModel
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return new Resultado<List<ProdutoViewModel>>()
                {
                    Mensagem = $"Erro ao listar os cliente: {ex.Message}!",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
        }
        public async Task<Resultado<ProdutoViewModel>> ObterClienteAsync(int id)
        {
            try
            {
                var produto = await _produtoRepository.ObterProdutoAsync(id);
                var produtoViewModel = _mapper.Map<ProdutoViewModel>(produto);
                var resultado = new Resultado<ProdutoViewModel>()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Model = produtoViewModel
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return Resultado<ProdutoViewModel>.ErroMensagem($"Erro ao obter o produto {ex.Message}!");
            }
        }
    }
}
