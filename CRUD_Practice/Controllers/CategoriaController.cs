using CRUD_Practice.Data;
using CRUD_Practice.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Practice.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly CategoriaDAL _categoriaDal;

        public CategoriaController(CategoriaDAL categoriaDAL)
        {
            _categoriaDal = categoriaDAL;
        }
        public IActionResult Index()
        {
            var categorias = _categoriaDal.ObtenerCategorias();
            return View(categorias);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoriaDal.InsertarCategoria(categoria);
                    TempData["Mensaje"] = "Categoría creada exitosamente.";
                    return RedirectToAction("Index");
                }
                return View(categoria);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al crear la categoría: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar crear la categoría. Por favor, inténtelo de nuevo.";
                
                return View(categoria);
            }
        }

        public IActionResult Editar(int id)
        {
            var categoria = _categoriaDal.ObtenerCategorias().FirstOrDefault(p => p.CategoriaID == id);
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Editar(Categoria categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoriaDal.ActualizarCategoria(categoria);
                    TempData["Mensaje"] = "Categoría editado con éxito.";
                    return RedirectToAction("Index");
                }
                return View(categoria);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al editar categoría: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar editar la categoría. Por favor, inténtelo de nuevo.";

                return View(categoria);
            }   
        }

        public IActionResult Eliminar(int id)
        {
            try
            {
                _categoriaDal.EliminarCategoria(id);
                TempData["Mensaje"] = "Categoría eliminada con éxito.";
                return RedirectToAction("Index");
            }
            catch(Exception ex) {
                Console.WriteLine($"Error al eliminar la Categoría: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar eliminar la Categoría. Por favor, inténtelo de nuevo.";

                return RedirectToAction("Index");
            }
        }
    }
}
