using ePizzaHub.Entities;
using Microsoft.AspNetCore.Identity;

namespace ePizzaHub.UI.Helpers.Services
{
    public class UserAccessor : IUserAccessor
    {
        private readonly UserManager<User> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public User GetUser()
        {
            if (_httpContextAccessor.HttpContext.User != null)
            {
                User user = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
                return user;
            }
                
            else
                return null;
        }
    }
}
