using CRUD_Practice.Data;
using CRUD_Practice.Models;
using CRUD_Practice.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_Practice.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly ProveedorDAL _proveedorDAL;

        public ProveedorController(ProveedorDAL proveedorDAL)
        {
            _proveedorDAL = proveedorDAL;
            
        }
        public IActionResult Index()
        {
            var proveedores = _proveedorDAL.ObtenerProveedores();
            return View(proveedores);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _proveedorDAL.InsertarProveedor(proveedor);
                return RedirectToAction("Index");
            }
            return View(proveedor);

        }

        public IActionResult Editar(int id)
        {
            var proveedor = _proveedorDAL.ObtenerProveedores().FirstOrDefault(p => p.ProveedorID == id);
            return View(proveedor);
        }

        [HttpPost]
        public IActionResult Editar(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _proveedorDAL.ActualizarProveedor(proveedor);
                TempData["Mensaje"] = "Proveedor editado con éxito.";
                return RedirectToAction("Index");
            }

            return View(proveedor);
        }

        public IActionResult Eliminar(int id)
        {
            _proveedorDAL.EliminarProveedor(id);
            TempData["Mensaje"] = "Proveedor eliminado con éxito.";
            return RedirectToAction("Index");
        }
    }
}
