using CRUD_Practice.Data;
using CRUD_Practice.DTO;
using CRUD_Practice.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_ADO.NET_Project.Controllers
{
    public class MarcaController : Controller
    {
        private readonly MarcaDAL _marcaDAL;

        public MarcaController(MarcaDAL marcaDAL)
        {
            _marcaDAL = marcaDAL;
        }
        public IActionResult Index()
        {
            var marcas = _marcaDAL.ObtenerMarcas();
            return View(marcas);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Marca marca)
        {
            if (ModelState.IsValid)
            {
                _marcaDAL.InsertarMarca(marca);
                return RedirectToAction("Index");
            }
            return View(marca);

        }

        public IActionResult Editar(int id)
        {
            var marca = _marcaDAL.ObtenerMarcas().FirstOrDefault(p => p.MarcaID == id);
            return View(marca);
        }

        [HttpPost]
        public IActionResult Editar(Marca marca)
        {
            _marcaDAL.ActualizarMarca(marca);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            _marcaDAL.EliminarMarca(id);
            return RedirectToAction("Index");
        }
    }
}
