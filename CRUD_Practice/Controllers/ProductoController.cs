using CRUD_Practice.Data;
using CRUD_Practice.Models;
using CRUD_Practice.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_ADO.NET_Project.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoDAL _productoDAL;
        private readonly MarcaDAL _marcaDAL;
        private readonly CategoriaDAL _categoriaDAL;

        public ProductoController(ProductoDAL productoDAL, MarcaDAL marcaDal, CategoriaDAL categoriaDAL)
        {
            _productoDAL = productoDAL;
            _marcaDAL = marcaDal;
            _categoriaDAL = categoriaDAL;
        }

        public IActionResult Index()
        {
            try
            {
                var productos = _productoDAL.ObtenerProductos();
                return View(productos);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al crear el producto: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar cargar los productos. Por favor, inténtelo de nuevo.";
                return View();
            }
        }

        public IActionResult Crear()
        {
            CargarMarcas();
            CargarCategorias();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(ProductoDTO producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productoDAL.InsertarProducto(producto);
                    TempData["Mensaje"] = "Producto creado exitosamente.";
                    return RedirectToAction("Index");
                }
                CargarMarcas();
                CargarCategorias();
                return View(producto);

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al crear el producto: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar crear el producto. Por favor, inténtelo de nuevo.";

                CargarMarcas();
                CargarCategorias();
                return View(producto);
            }

        }

        public IActionResult Editar(int id)
        {
            var producto = _productoDAL.ObtenerProductos().FirstOrDefault(p => p.ProductoID == id);
            CargarMarcas();
            CargarCategorias();
            return View(producto);
        }

        [HttpPost]
        public IActionResult Editar(ProductoDTO producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _productoDAL.ActualizarProducto(producto);
                    TempData["Mensaje"] = "Producto editado con éxito.";
                    return RedirectToAction("Index");
                }
                return View(producto);

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al editar el producto: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar editar el producto. Por favor, inténtelo de nuevo.";
                
                return View(producto);
            }
        }

        public IActionResult Eliminar(int id)
        {
            try
            {
                _productoDAL.EliminarProducto(id);
                TempData["Mensaje"] = "Producto eliminado con éxito.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el producto: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar eliminar el producto. Por favor, inténtelo de nuevo.";

                return RedirectToAction("Index");
            }  
        }

        private void CargarMarcas()
        {
            var marcas = _marcaDAL.ObtenerMarcas();
            ViewBag.Marcas = new SelectList(marcas, "MarcaID", "Nombre");
        }

        private void CargarCategorias()
        {
            var categorias = _categoriaDAL.ObtenerCategorias();
            ViewBag.Categorias = new SelectList(categorias, "CategoriaID", "Nombre");
        }

    }
}

