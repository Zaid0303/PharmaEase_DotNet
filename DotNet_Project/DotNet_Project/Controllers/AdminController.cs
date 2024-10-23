using DotNet_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Project.Controllers
{
    public class AdminController : Controller
    {
        PharmacyContext db = new PharmacyContext();
        public IActionResult Index()
        {
            return View();
        }

        //-------- Admin Role Start --------//
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddRole(Role rg)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(rg);
                db.SaveChanges();

            }

            return View();
        }
        public IActionResult ShowRole()
        {
            var Rolekadata = db.Roles.ToList();
            return View(Rolekadata);
        }

        [HttpGet]
        public IActionResult DeleteRole(Role rg)
        {

            db.Roles.Remove(rg);
            db.SaveChanges();
            return RedirectToAction("ShowRole");
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
        //-------- Admin Role End --------//


        //-------- Admin Category Start --------//
        [HttpGet]
        public IActionResult AddCat()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCat(Category cg, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var imageName = Path.GetFileName(file.FileName);
                string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Image/Cat_img/");
                string imagevalue = Path.Combine(imagePath, imageName);
                using (var stream = new FileStream(imagevalue, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbimage = Path.Combine("/Image/Cat_img/", imageName);
                cg.Image = dbimage;
                db.Categories.Add(cg);
                db.SaveChanges();
            }
            return View();
        }
        public IActionResult ShowCat()
        {
            var catkadata = db.Categories.ToList();
            return View(catkadata);
        }

        [HttpGet]
        public IActionResult DeleteCat(Category cg)
        {

            db.Categories.Remove(cg);
            db.SaveChanges();
            return RedirectToAction("ShowCat");

        }


        [HttpGet]
        public IActionResult EditCat(int Id)
        {
            var category = db.Categories.Find(Id);
            return View(category);
        }
        [HttpPost]
        public IActionResult EditCat(Category cg, IFormFile file, string catid)
        {
            if (ModelState.IsValid)
            {
                var dbimg = "";
                if (file != null && file.Length > 0)
                {
                    var imageName = Path.GetFileName(file.FileName);
                    string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Image/Cat_img/");
                    string imagevalue = Path.Combine(imagePath, imageName);
                    using (var stream = new FileStream(imagevalue, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    dbimg = Path.Combine("/Image/Cat_img/", imageName);
                    cg.Image = dbimg;
                    db.Categories.Update(cg);
                    db.SaveChanges();
                }
                else {
                    cg.Image = catid;
                    db.Categories.Update(cg);
                    db.SaveChanges();

                }
            }
                return RedirectToAction("ShowCat");

        }

        //-------- Admin Category End --------//

        //-------- Admin Category End --------//
        //-------- Admin Category End --------//


    }

}