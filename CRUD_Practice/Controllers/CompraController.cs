using CRUD_Practice.Data;
using CRUD_Practice.Models;
using CRUD_Practice.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_ADO.NET_Project.Controllers
{
    public class CompraController : Controller
    {
        private readonly CompraDAL _compraDAL;
        private readonly ProveedorDAL _proveedorDal;
        private readonly ProductoDAL _productoDal;

        public CompraController(CompraDAL compraDAL, ProveedorDAL proveedorDAL, ProductoDAL productoDAL)
        {
            _compraDAL = compraDAL;
            _proveedorDal = proveedorDAL;
            _productoDal = productoDAL;

        }
        public IActionResult Crear()
        {
            CargarProveedores();
            CargarProductos();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Compra compra)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _compraDAL.InsertarCompra(compra);
                    TempData["Mensaje"] = "Compra creada exitosamente.";
                    return RedirectToAction("Crear");
                }
                CargarProveedores();
                CargarProductos();
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear compra: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar crear la compra. Por favor, inténtelo de nuevo.";

                CargarProveedores();
                CargarProductos();
                return View();
            }

        }

        private void CargarProveedores()
        {
            var proveedores = _proveedorDal.ObtenerProveedores();
            ViewBag.Proveedores = new SelectList(proveedores, "ProveedorID", "Nombre");
        }

        private void CargarProductos()
        {
            var productos = _productoDal.ObtenerProductos();
            ViewBag.Productos = new SelectList(productos, "ProductoID", "Nombre");
        }
    }
}
