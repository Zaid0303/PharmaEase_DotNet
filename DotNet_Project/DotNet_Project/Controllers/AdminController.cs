using DotNet_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace DotNet_Project.Controllers
{
    public class AdminController : Controller
    {
        PharmacyContext db = new PharmacyContext();
        public IActionResult Index()
        {
            // Fetch counts from the database
            var totalProducts = db.Products.Count();
            var totalOrderDetails = db.OrderDetails.Count();
            var totalCategories = db.Categories.Count();
            var totalRole2Users = db.Logins.Count(u => u.RoleId == 2);

            // Fetch minimum and maximum PKR prices from OrderDetails
            var minOrderPrice = db.OrderDetails.Min(o => o.ProId);
            var maxOrderPrice = db.OrderDetails.Max(o => o.ProId);

            // Pass the counts and order prices to the view using ViewData
            ViewData["TotalProducts"] = totalProducts;
            ViewData["TotalOrderDetails"] = totalOrderDetails;
            ViewData["TotalCategories"] = totalCategories;
            ViewData["TotalUsers"] = totalRole2Users;
            ViewData["MinOrderPrice"] = minOrderPrice;
            ViewData["MaxOrderPrice"] = maxOrderPrice;

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


        //-------- Admin User Start --------//
        public IActionResult ShowUser()
        {
            var Userkadata = db.Logins.ToList();
            return View(Userkadata);
        }

        [HttpGet]
        public IActionResult DeleteUser(Login Ug)
        {

            db.Logins.Remove(Ug);
            db.SaveChanges();
            return RedirectToAction("ShowUser");
        }
        //-------- Admin User End --------//


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

        //-------- Admin Job Start --------//

        [HttpGet]
        public IActionResult AddJob()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddJob(Job jg, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var imageName = Path.GetFileName(file.FileName);
                string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Image/Job_img/");
                string imagevalue = Path.Combine(imagePath, imageName);
                using (var stream = new FileStream(imagevalue, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbimage = Path.Combine("/Image/Job_img/", imageName);
                jg.Image = dbimage;
                db.Jobs.Add(jg);
                db.SaveChanges();
            }
            return View();
        }
        public IActionResult ShowJob()
        {
            var jobkadata = db.Jobs.ToList();
            return View(jobkadata);
        }

        [HttpGet]
        public IActionResult DeleteJob(Job jg)
        {

            db.Jobs.Remove(jg);
            db.SaveChanges();
            return RedirectToAction("ShowJob");

        }

        [HttpGet]
        public IActionResult EditJob(int Id)
        {
            var job = db.Jobs.Find(Id);
            return View(job);
        }
        [HttpPost]
        public IActionResult EditJob(Job jg, IFormFile file, string jobid)
        {
            if (ModelState.IsValid)
            {
                var dbimg = "";
                if (file != null && file.Length > 0)
                {
                    var imageName = Path.GetFileName(file.FileName);
                    string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Image/Job_img/");
                    string imagevalue = Path.Combine(imagePath, imageName);
                    using (var stream = new FileStream(imagevalue, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    dbimg = Path.Combine("/Image/Job_img/", imageName);
                    jg.Image = dbimg;
                    db.Jobs.Update(jg);
                    db.SaveChanges();
                }
                else
                {
                    jg.Image = jobid;
                    db.Jobs.Update(jg);
                    db.SaveChanges();

                }
            }
            return RedirectToAction("ShowJob");

        }

        //-- Show User Apply Jobs --//
        public IActionResult ShowApplyJob()
        {
            var jobApplications = db.ApplyJobs
                                    .Include(a => a.Job)
                                    .Include(a => a.User)
                                    .ToList();
            return View(jobApplications);
        }

        public IActionResult Approve(int id)
        {
            var application = db.ApplyJobs.Include(a => a.User).FirstOrDefault(a => a.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            application.Status = "Approved";
            db.SaveChanges();

            SendStatusEmail(application.User.Email, application.Status);
            return RedirectToAction("ShowApplyJob");
        }

        public IActionResult Reject(int id)
        {
            var application = db.ApplyJobs.Include(a => a.User).FirstOrDefault(a => a.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            application.Status = "Rejected";
            db.SaveChanges();

            SendStatusEmail(application.User.Email, application.Status);
            return RedirectToAction("ShowApplyJob");
        }

        private void SendStatusEmail(string userEmail, string status)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("pharmaease03@gmail.com", "cpeyztunyvumuupf")
            };

            MailMessage msg = new MailMessage("pharmaease03@gmail.com", userEmail)
            {
                Subject = "Application Status Update",
                Body = $"Dear Applicant,\n\nYour application status has been updated to: {status}.\n\nThank you for applying.\n\nBest Regards,\nPharmaEase Team"
            };

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

    //-- Show User Apply Jobs --//


    //-------- Admin Job End --------//

    //-------- Admin company start --------//

    [HttpGet]
        public IActionResult AddComp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddComp(Company cc, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var imageName = Path.GetFileName(file.FileName);
                string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Image/Company_img/");
                string imagevalue = Path.Combine(imagePath, imageName);
                using (var stream = new FileStream(imagevalue, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbimage = Path.Combine("/Image/Company_img/", imageName);
                cc.Logo = dbimage;
                db.Companies.Add(cc);
                db.SaveChanges();
            }
            return View();
        }

        //show/

        public IActionResult ShowComp()
        {
            var compkadata = db.Companies.ToList();
            return View(compkadata);
        }

        //delete//

        [HttpGet]
        public IActionResult DeleteComp(Company cc)
        {

            db.Companies.Remove(cc);
            db.SaveChanges();
            return RedirectToAction("ShowComp");

        }

        //update//
        [HttpGet]
        public IActionResult EditComp(int Id)
        {
            var company = db.Companies.Find(Id);
            return View(company);
        }
        [HttpPost]
        public IActionResult EditComp(Company cc, IFormFile file, string compid)
        {
            if (ModelState.IsValid)
            {
                var dbimg = "";
                if (file != null && file.Length > 0)
                {
                    var imageName = Path.GetFileName(file.FileName);
                    string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Image/Company_img/");
                    string imagevalue = Path.Combine(imagePath, imageName);
                    using (var stream = new FileStream(imagevalue, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    dbimg = Path.Combine("/Image/Company_img/", imageName);
                    cc.Logo = dbimg;
                    db.Companies.Update(cc);
                    db.SaveChanges();
                }
                else
                {
                    cc.Logo = compid;
                    db.Companies.Update(cc);
                    db.SaveChanges();

                }
            }
            return RedirectToAction("ShowComp");

        }

        //-------- Admin company end  --------//

        //-------- Admin Product Start --------//
        [HttpGet]
        public IActionResult AddPro()
        {
            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddPro(Product pg, IFormFile file)
        {
                var imageName = Path.GetFileName(file.FileName);
                string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Image/Pro_img/");
                string imagevalue = Path.Combine(imagePath, imageName);
                using (var stream = new FileStream(imagevalue, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var dbimage = Path.Combine("/Image/Pro_img/", imageName);
                pg.Image = dbimage;
                db.Products.Add(pg);
                db.SaveChanges();

            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }
        public IActionResult ShowPro()
        {
            var productkadata = db.Products.Include(p => p.CIdNavigation);
            return View(productkadata.ToList());
        }


        [HttpGet]
        public IActionResult DeletePro(Product pg)
        {

            db.Products.Remove(pg);
            db.SaveChanges();
            return RedirectToAction("ShowPro");

        }


        [HttpGet]
        public IActionResult EditPro(int Id)
        {
            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name");
            var product = db.Products.Find(Id);
            return View(product);
        }
        [HttpPost]
        public IActionResult EditPro(Product pg, IFormFile file, string productid)
        {
                var dbimg = "";
                if (file != null && file.Length > 0)
                {
                    var imageName = Path.GetFileName(file.FileName);
                    string imagePath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Image/Pro_img/");
                    string imagevalue = Path.Combine(imagePath, imageName);
                    using (var stream = new FileStream(imagevalue, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    dbimg = Path.Combine("/Image/Pro_img/", imageName);
                    pg.Image = dbimg;
                    db.Products.Update(pg);
                    db.SaveChanges();
                }
                else
                {
                    pg.Image = productid;
                    db.Products.Update(pg);
                    db.SaveChanges();

                }
            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name");
            return RedirectToAction("ShowPro");

        }

        //-------- Admin Product End --------//


        //-------- Admin Order & Details Start --------//
        //[Authorize(Roles = "Admin")]
        public IActionResult ShowOrder()
        {
            // Get all orders with their associated user
            var orders = db.Orders.Include(p => p.User).ToList();
            return View(orders);
        }

        public IActionResult UpdateOrderStatus(int id, string status)
        {
            var order = db.Orders.Include(o => o.User).FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                // Update the order status
                order.OrderStatus = status;
                db.SaveChanges();

                // Send email confirmation to the user
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("pharmaease03@gmail.com", "cpeyztunyvumuupf")
                };

                string emailBody = $"Dear {order.User.Name},\n\n" +
                                   $"Your order with Order ID: {order.Id} has been {status}.\n\n" +
                                   $"Order Details:\n" +
                                   $"- Total Price: {order.TotalAmount}\n" +
                                   $"- Status: {status}\n\n" +
                                   $"Thank you for shopping with us.";

                MailMessage msg = new MailMessage("pharmaease03@gmail.com", order.User.Email)
                {
                    Subject = $"Order {status}",
                    Body = emailBody
                };

                client.Send(msg);
            }

            // Show success message
            ViewBag.Message = $"Order {status} & email notification sent successfully.";

            return RedirectToAction("ShowOrder");
        }

        public IActionResult ShowOrderDetail()
        {
            var orderDetails = db.OrderDetails.Include(x => x.Pro)
                                              .Include(x => x.User)
                                              .Include(x => x.Order)
                                              .ToList();
            return View(orderDetails);
        }

        //-------- Admin Order & Details End --------//


        //-------- Admin Contact Start --------//
        public IActionResult ShowContact()
        {
            var Contactkadata = db.Contacts.ToList();
            return View(Contactkadata);
        }

        //-------- Admin Contact End --------//

    }

}