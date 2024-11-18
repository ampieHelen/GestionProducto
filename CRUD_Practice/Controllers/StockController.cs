using Microsoft.AspNetCore.Mvc;

namespace CRUD_Practice.Controllers
{
    public class StockController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
