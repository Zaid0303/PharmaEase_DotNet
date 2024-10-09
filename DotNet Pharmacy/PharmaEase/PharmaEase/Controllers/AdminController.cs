using Microsoft.AspNetCore.Mvc;

namespace PharmaEase.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult ShowProduct()
        {
            return View();
        }

        public IActionResult EditProduct()
        {
            return View();
        }

        public IActionResult DeleteProduct()
        {
            return View();
        }
    }
}
