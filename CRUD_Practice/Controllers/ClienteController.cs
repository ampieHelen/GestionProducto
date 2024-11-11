using CRUD_Practice.Data;
using CRUD_Practice.Models;
using CRUD_Practice.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_Practice.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteDAL _clienteDAL;

        public ClienteController(ClienteDAL clienteDAL)
        {
            _clienteDAL = clienteDAL;

        }
        public IActionResult Index()
        {
            var clientes = _clienteDAL.ObtenerClientes();
            return View(clientes);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteDAL.InsertarCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);

        }

        public IActionResult Editar(int id)
        {
            var cliente = _clienteDAL.ObtenerClientes().FirstOrDefault(p => p.ClienteID == id);
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Editar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _clienteDAL.ActualizarCliente(cliente);
                TempData["Mensaje"] = "Cliente editado con éxito.";
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        public IActionResult Eliminar(int id)
        {
            _clienteDAL.EliminarCliente(id);
            TempData["Mensaje"] = "Cliente eliminado con éxito.";
            return RedirectToAction("Index");
        }
    }
}
