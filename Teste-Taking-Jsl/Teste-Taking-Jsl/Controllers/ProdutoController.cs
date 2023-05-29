using Microsoft.AspNetCore.Mvc;
using Teste_Taking_Jsl.Application.Interfaces;
using Teste_Taking_Jsl.Application.Models;

namespace Teste_Taking_Jsl.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;
        public ProdutoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpGet]
        //esta action retornar a lista de produtos ativos e inativos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ListarProdutos()
        {
            var lstProdutos = await _produtoAppService.ListarProdutoAsync();
            return Json(lstProdutos);
        }

        [HttpGet]
        public ActionResult CadastrarProduto()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SalvarProduto(ProdutoViewModel produtoViewModel)
        {
            try
            {
                if (produtoViewModel.Id > default(int))
                    await _produtoAppService.AtualizarProdutoAsync(produtoViewModel);
                else
                    await _produtoAppService.SalvarProdutoAsync(produtoViewModel);
                return RedirectToAction("Index", "Produto");
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public async Task<ActionResult> EditarProduto(int id)
        {
            var resultado = await _produtoAppService.ObterClienteAsync(id);
            ViewBag.Sucess = resultado.Mensagem;
            return View("CadastrarProduto", resultado.Model);
        }

        [HttpGet]
        public async Task<ActionResult> DeletarProduto(int id, IFormCollection collection)
        {
            _ = await _produtoAppService.DeletarProdutoAsync(id);
            return RedirectToAction("Index", "Produto");
        }
    }
}
