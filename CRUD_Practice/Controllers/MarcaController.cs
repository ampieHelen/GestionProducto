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
            try
            {
                if (ModelState.IsValid)
                {
                    _marcaDAL.InsertarMarca(marca);
                    TempData["Mensaje"] = "Marca creada exitosamente.";
                    return RedirectToAction("Index");
                }
                return View(marca);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la marca: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar crear la marca. Por favor, inténtelo de nuevo.";

                return View(marca);
            }
        }

        public IActionResult Editar(int id)
        {
            var marca = _marcaDAL.ObtenerMarcas().FirstOrDefault(p => p.MarcaID == id);
            return View(marca);
        }

        [HttpPost]
        public IActionResult Editar(Marca marca)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _marcaDAL.ActualizarMarca(marca);
                    TempData["Mensaje"] = "Marca editado con éxito.";
                    return RedirectToAction("Index");
                }
                return View(marca);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al editar marca: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar editar la marca. Por favor, inténtelo de nuevo.";

                return View(marca);
            }
        }

        public IActionResult Eliminar(int id)
        {
            try
            {
                _marcaDAL.EliminarMarca(id);
                TempData["Mensaje"] = "Marca eliminada con éxito.";
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar eliminar la Marca. Por favor, inténtelo de nuevo.";

                return RedirectToAction("Index");
            }
        }
    }
}
