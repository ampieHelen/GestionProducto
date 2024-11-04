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
            if (ModelState.IsValid)
            {
                _categoriaDal.InsertarCategoria(categoria);
                return RedirectToAction("Index");
            }
            return View(categoria);

        }

        public IActionResult Editar(int id)
        {
            var categoria = _categoriaDal.ObtenerCategorias().FirstOrDefault(p => p.CategoriaID == id);
            return View(categoria);
        }

        [HttpPost]
        public IActionResult Editar(Categoria categoria)
        {
            _categoriaDal.ActualizarCategoria(categoria);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            _categoriaDal.EliminarCategoria(id);
            return RedirectToAction("Index");
        }
    }
}
