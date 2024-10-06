using laboratorna6_schedrov.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace laboratorna6_schedrov.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (user.Age >= 16)
            {
                return RedirectToAction("ProductForm", "Order", new { productQuantity = 1 });
            }
            else
            {
                ViewBag.Message = "You must be at least 16 y.o. to make an order!";
                return View(user);
            }
        }
    }
}
