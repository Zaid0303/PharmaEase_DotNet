using DotNet_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Project.Controllers
{
    public class UserController : Controller
    {
        PharmacyContext db = new PharmacyContext();

        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }
        [HttpGet]
        public IActionResult ShowProducts(int Id)
        {
            //var productkadata = db.Products.Include(p => p.CIdNavigation);
            //return View(productkadata.ToList());
            var resultt = db.Products.Where(x => x.CId.Equals(Id));
            return View(resultt);
        }

        public IActionResult MyShop(int? categoryId)
        {
            // Get all categories with their product counts
            var categoriesWithCounts = db.Categories
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    ProductCount = c.Products.Count()
                })
                .ToList();

            ViewBag.CategoriesWithCounts = categoriesWithCounts;

            // If a category is selected, filter products by category
            var products = categoryId.HasValue
                ? db.Products.Where(p => p.CId == categoryId).ToList()
                : db.Products.ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult ProDetails(int Id)
        {

            var resultt = db.Products.Where(x => x.Id.Equals(Id));
            if (resultt == null)
            {
                return NotFound();
            }
            return View(resultt);


        }




        public IActionResult ProDetails()
        {

            return View(db.Products.ToList());
        }


        //-------- Contact Start --------//
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact rg)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(rg);
                db.SaveChanges();

            }

            return View();
        }
        public IActionResult ShowContact()
        {
            var Contactkadata = db.Contacts.ToList();
            return View(Contactkadata);
        }

        [HttpGet]
        public IActionResult DeleteContact(Contact rg)
        {

            db.Contacts.Remove(rg);
            db.SaveChanges();
            return RedirectToAction("ShowContact");
        }


        [HttpGet]
        public IActionResult EditRole(int Id)
        {
            var deletekadata = db.Roles.Find(Id);
            return View(deletekadata);
        }
        [HttpPost]
        public IActionResult EditRole(Role rg)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Update(rg);
                db.SaveChanges();

            }

            return View();
        }
        //-------- Contact End --------//


    }
}
