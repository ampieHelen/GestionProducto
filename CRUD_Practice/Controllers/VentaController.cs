using CRUD_Practice.Data;
using CRUD_Practice.Models;
using CRUD_Practice.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_ADO.NET_Project.Controllers
{
    public class VentaController : Controller
    {
        private readonly VentaDal _ventaDal;
        private readonly ClienteDAL _clienteDAL;
        private readonly StockDAL _stockDAL;

        public VentaController(VentaDal ventaDal, ClienteDAL clienteDAL, StockDAL stockDAL)
        {
            _ventaDal = ventaDal;
            _clienteDAL = clienteDAL;
            _stockDAL = stockDAL;

        }

        public IActionResult Crear()
        {
            CargarClientes();
            CargarProductosDeStock();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Venta venta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ventaDal.InsertarVenta(venta);
                    TempData["Mensaje"] = "Venta creada exitosamente.";
                    return RedirectToAction("Crear");
                }
                CargarClientes();
                CargarProductosDeStock();
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear venta: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar crear la venta. Por favor, inténtelo de nuevo.";

                CargarClientes();
                CargarProductosDeStock();
                return View();
            }

        }
        private void CargarClientes()
        {
            var clientes = _clienteDAL.ObtenerClientes();
            ViewBag.Clientes = new SelectList(clientes, "ClienteID", "Nombre");
        }

        private void CargarProductosDeStock()
        {
            var productos = _stockDAL.ObtenerStock();
            ViewBag.Productos = new SelectList(productos, "ProductoID", "Producto", "Precio");
        }

        [HttpGet]
        public IActionResult ObtenerPrecioProducto(int productoID)
        {
            if (productoID <= 0)
                return BadRequest("Producto inválido.");

            // Usar el método ObtenerStock para buscar el precio
            var stock = _stockDAL.ObtenerStock();
            var producto = stock.FirstOrDefault(p => p.ProductoID == productoID);

            if (producto == null)
                return NotFound("Producto no encontrado.");

            return Json(new { precio = producto.Precio });
        }

    }
}
