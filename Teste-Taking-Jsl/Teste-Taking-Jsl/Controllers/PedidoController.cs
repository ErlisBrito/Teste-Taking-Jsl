using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Teste_Taking_Jsl.Application.Interfaces;
using Teste_Taking_Jsl.Application.Models;

namespace Teste_Taking_Jsl.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IClienteAppService _clienteAppService;
        private readonly IProdutoAppService _produtoAppService;
        private readonly IPedidoAppService _pedidoAppService;

        public PedidoController(IClienteAppService clienteAppService, IProdutoAppService produtoAppService, IPedidoAppService pedidoAppService)
        {
            _clienteAppService = clienteAppService;
            _produtoAppService = produtoAppService;
            _pedidoAppService = pedidoAppService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> ListarPedidos()
        {
            var lstPedidos = await _pedidoAppService.ListarPedidoAsync();
            return Json(lstPedidos);
        }
    

        [HttpGet]
        public async Task<ActionResult> CadastrarPedidoAsync()
        {

            ViewBag.Cliente = await ObterCliente();
            ViewBag.Produto = await ObterProduto();
            ViewBag.Status = await ObterStatus();
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> SalvarPedido(PedidoViewModel pedidoViewModel)
        {
            try
            {
                if (pedidoViewModel.Id > default(int))
                    await _pedidoAppService.AtualizarPedidoAsync(pedidoViewModel);
                else
                    await _pedidoAppService.SalvarPedidoAsync(pedidoViewModel);
                return RedirectToAction("Index", "Pedido");
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        [HttpGet]
        public async Task<ActionResult> EditarPedido(int id)
        {
            var resultado = await _pedidoAppService.ObterPedidoAsync(id);
            ViewBag.Cliente = await ObterCliente();
            ViewBag.Produto = await ObterProduto();
            ViewBag.Status = await ObterStatus();
            return View("CadastrarPedido", resultado.Model);
        }


       
        [HttpGet]
        public async Task<ActionResult> DetalheCliente(int clienteId)
        {
            var resultado = await _clienteAppService.ObterClienteAsync(clienteId);
            return View(resultado.Model);
        }

        [HttpGet]
        public async Task<ActionResult> DetalheProduto(int produtoId)
        {
            var resultado = await _produtoAppService.ObterClienteAsync(produtoId);
            return View(resultado.Model);
        }

        [HttpGet]
        public async Task<ActionResult> DeletarPedido(int id)
        {
            _ = await _pedidoAppService.DeletarPedidoAsync(id);
            return RedirectToAction("Index", "Pedido");
        }

        private async Task<List<SelectListItem>> ObterCliente()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            var lstCliente = await _clienteAppService.ListarClienteAtivoAsync();
            foreach (var item in lstCliente.Model)
            {
                string clienteId = item.Id.ToString();
                selectListItems.Add(new SelectListItem() { Text = item.Nome, Value = clienteId });
            }

            return selectListItems;
        }

        private async Task<List<SelectListItem>> ObterProduto()
        {
            var lstProduto = await _produtoAppService.ListarProdutoAtivoAsync();
            var lstItem = new List<SelectListItem>();
            foreach (var item in lstProduto.Model)
            {
                lstItem.Add(new SelectListItem() { Text = item.Nome, Value = item.Id.ToString() });
            }
            return lstItem;
        }
        private async Task<List<SelectListItem>> ObterStatus()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Novo", Value = "novo"},
                new SelectListItem {Text = "Atendido", Value = "atendido"},
                new SelectListItem {Text = "Em rota", Value = "em rota"}
            };
        }
    }
}
