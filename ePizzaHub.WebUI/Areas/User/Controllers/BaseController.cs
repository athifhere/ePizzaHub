using ePizzaHub.UI.Helpers.Services;
using ePizzaHub.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.Areas.User.Controllers
{
    [CustomAuthorize(Roles = "User")]
    [Area("User")]
    public class BaseController : Controller
    {
        protected Entities.User CurrentUser
        {
            get
            {
                if (User != null)
                    return _userAccessor.GetUser();
                else
                    return null;
            }
        }

        private IUserAccessor _userAccessor;
        public BaseController(IServiceProvider serviceProvider)
        {
            _userAccessor = serviceProvider.GetService<IUserAccessor>();
        }
    }
}
