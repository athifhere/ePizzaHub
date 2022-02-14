using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
