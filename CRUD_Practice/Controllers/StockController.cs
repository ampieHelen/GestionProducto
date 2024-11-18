using CRUD_Practice.Data;
using CRUD_Practice.Models;
using CRUD_Practice.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_ADO.NET_Project.Controllers
{
    public class StockController : Controller
    {
        private readonly StockDAL _stockDAL;

        public StockController(StockDAL stockDAL)
        {
            _stockDAL = stockDAL;
        }

        public IActionResult Index()
        {
            try
            {
                var stock = _stockDAL.ObtenerStock();
                return View(stock);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el stock: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al intentar cargar el stock. Por favor, inténtelo de nuevo.";
                return View();
            }
        }
    }
}
