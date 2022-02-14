using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public User AuthenticateUser(string username, string password)
        {
            var result = _signInManager.PasswordSignInAsync(username, password, false, false).Result;
            if(result.Succeeded)
            {
                User user = _userManager.FindByNameAsync(username).Result;
                var roles = _userManager.GetRolesAsync(user).Result;
                user.Roles = roles.ToArray();
                return user;
            }
            return null;
        }

        public bool CreateUser(User user, string password, string role)
        {
            var result = _userManager.CreateAsync(user, password).Result;
            if(result.Succeeded)
            {
                var result2 = _userManager.AddToRoleAsync(user, role).Result;
                if(result2.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public bool SignOut()
        {
            try
            {
                _signInManager.SignOutAsync().Wait();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
