using ePizzaHub.Entities;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Login()
        {
            if (TempData["flag"] != null)
            {
                if ((bool)TempData["flag"] == true)
                {
                    ViewBag.flag = true;
                }
            }
            else
            {
                ViewBag.flag = false;
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _authService.AuthenticateUser(model.Email, model.Password);
                if (user.Roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else if (user.Roles.Contains("User"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "User" });
                }
            }
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(UserModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber
                };
                bool result = _authService.CreateUser(user, model.Password, "User");
                if (result)
                {
                    TempData["flag"] = true;
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        public IActionResult SignOut()
        {
            _authService.SignOut();
            return RedirectToAction("SignOutSuccessfully");
        }

        public IActionResult SignOutSuccessfully()
        {
            return View();
        }
    }
}
