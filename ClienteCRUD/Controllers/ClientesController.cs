using ClienteCRUD.Models;
using ClienteCRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClienteCRUD.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteService _service;

        public ClientesController(ClienteService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var clientes = _service.ObterTodos();
            return View(clientes);
        }

        public IActionResult Details(int id)
        {
            var cliente = _service.ObterPorId(id);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _service.Salvar(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public IActionResult Edit(int id)
        {
            var cliente = _service.ObterPorId(id);
            if (cliente is null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _service.Salvar(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        public IActionResult Delete(int id)
        {
            var cliente = _service.ObterPorId(id);
            if (cliente is null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Deletar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
