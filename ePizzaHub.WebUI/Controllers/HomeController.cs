﻿using ePizzaHub.Services.Interfaces;
using ePizzaHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ePizzaHub.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;

        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public IActionResult Index()
        {
            try
            {
                int x = 0, y = 3;
                int z = y / x;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            var data = _catalogService.GetItems();
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}