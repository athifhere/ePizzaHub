using ePizzaHub.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ePizzaHub.WebUI.Controllers
{
    public class ProductController : Controller
    {
        HttpClient _client;
        IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            Uri baseAddress = new Uri(_configuration["ApiAddress"]);
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        public IActionResult Index()
        {
            IEnumerable<ProductModel> model = new List<ProductModel>();
            var response = _client.GetAsync(_client.BaseAddress + "/product").Result;
            if(response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                model = JsonSerializer.Deserialize<IEnumerable<ProductModel>>(data);
            }
            return View(model);
        }
    }
}
