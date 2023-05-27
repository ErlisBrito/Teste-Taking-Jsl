using Microsoft.AspNetCore.Mvc;
using Teste_Taking_Jsl.Application.Interfaces;
using Teste_Taking_Jsl.Application.Models;

namespace Teste_Taking_Jsl.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteAppService _clienteAppService;
        public ClienteController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }


        [HttpGet]
        //esta action retornar a lista de clientes ativos e inativos
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ListarCliente()
        {
            var lstCliente = await _clienteAppService.ListarClienteAsync();
            return Json(lstCliente);
        }

        [HttpGet]
        public ActionResult CadastrarCliente()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SalvarCliente(ClienteViewModel clienteViewModel)
        {
            try
            {
                if (clienteViewModel.Id > default(int))
                    await _clienteAppService.AtualizarClienteAsync(clienteViewModel);
                else
                    await _clienteAppService.SalvarClienteAsync(clienteViewModel);
                return RedirectToAction("Index", "Cliente");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarCliente(int id)
        {
            var resultado = await _clienteAppService.ObterClienteAsync(id);
            ViewBag.Sucess = resultado.Mensagem;
            return RedirectToAction("Index", "Cliente");
        }



        [HttpGet]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            _ = await _clienteAppService.DeletarClienteAsync(id);
            return RedirectToAction("Index", "Cliente");
        }
    }
}
