using ePizzaHub.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    [Area("Admin")]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
