using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.Areas.User.Controllers
{
    public class DashboardController : BaseController
    {
        public DashboardController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
