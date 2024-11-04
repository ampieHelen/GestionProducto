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
            var productos = _productoDAL.ObtenerProductos();
            return View(productos);
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
            if (ModelState.IsValid)
            {
                _productoDAL.InsertarProducto(producto);
                return RedirectToAction("Index");
            }
            CargarMarcas();
            CargarCategorias();
            return View(producto);

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
            _productoDAL.ActualizarProducto(producto);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            _productoDAL.EliminarProducto(id);
            return RedirectToAction("Index");
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

