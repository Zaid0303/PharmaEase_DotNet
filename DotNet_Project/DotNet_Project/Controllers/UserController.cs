using DotNet_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;

namespace DotNet_Project.Controllers
{
    public class UserController : Controller
    {
        PharmacyContext db = new PharmacyContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        //---- Dynamic Category Navbar ----//
        [HttpGet]
        public IActionResult showCatproducts(int Id)
        {

            var resultt = db.Products.Where(x => x.CId.Equals(Id));
            return View(resultt);
        }
        //---- Dynamic Category Navbar ----//



        //---- Show All Products ----//
        public IActionResult MyShop(int? categoryId, string xyz)
        {
            var categoriesWithCounts = db.Categories
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    ProductCount = c.Products.Count()
                })
                .ToList();

            ViewBag.CategoriesWithCounts = categoriesWithCounts;

            var productsQuery = db.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CId == categoryId);
            }

            if (!string.IsNullOrEmpty(xyz))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(xyz) ||
                    p.Description.Contains(xyz)
                );
            }

            var products = productsQuery.ToList();

            return View(products);
        }
        //---- Show All Products ----//



        //---- Products Details page ----// 

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
        //---- Products Details page ----// 



        //---- Cart -----//

        [Authorize]
        public IActionResult cart(int Id, Cart cc)
        {

            cc.Id = 0;
            String abc = User.FindFirst(ClaimTypes.Sid)?.Value;

            var upcart = db.Carts.FirstOrDefault(x => x.Userid == Convert.ToInt32(abc) && x.Proid == Id);


            var cartt = db.Products.Where(x => x.Id.Equals(Id));

            if (upcart == null)
            {
                foreach (var c in cartt)
                {


                    cc.Userid = Convert.ToInt32(abc);
                    cc.Proid = c.Id;
                    cc.Qty = 1;


                    cc.Proprice = c.Price;


                }
                db.Carts.Add(cc);

            }

            else if (upcart.Qty < 10)
            {
                foreach (var c in cartt)
                {

                    upcart.Qty += 1;
                    upcart.Proprice = c.Price * upcart.Qty;



                }
                db.Carts.Update(upcart);
            }
            else
            {
                TempData["already"] = "out of range";

            }

            db.SaveChanges();

            return RedirectToAction("ShowCart");
        }

        public IActionResult ShowCart()
        {

            string abc = User.FindFirst(ClaimTypes.Sid)?.Value;
            //var cartt = db.Carts.ToList();
            var cartt = db.Carts.Where(x => x.Userid.Equals(Convert.ToInt32(abc))).Include(x => x.Pro);
            ViewBag.Total = db.Carts.Where(x => x.Userid == Convert.ToInt32(abc)).Sum(x => x.Proprice);
            return View(cartt);

        }

        public ActionResult cartEdit(IFormCollection f)
        {
            var fid = f["ii"];
            var fqt = f["qq"];

            int ffid = Convert.ToInt32(fid);
            int ffqt = Convert.ToInt32(fqt);

            string abc = User.FindFirst(ClaimTypes.Sid)?.Value;

            var upcart = db.Carts.FirstOrDefault(x => x.Userid == Convert.ToInt32(abc) && x.Id == ffid);


            var pro = db.Products.FirstOrDefault(x => x.Id == upcart.Proid);


            if (upcart != null)
            {

                upcart.Qty = ffqt;
                int ffprice = Convert.ToInt32(pro.Price);
                int fprice = ffprice * ffqt;
                upcart.Proprice = fprice;
                db.Carts.Update(upcart);
                db.SaveChanges();

            }
            else
            {
                TempData["sorry"] = "sorry no data";
            }

            return RedirectToAction("ShowCart");
        }

        public IActionResult cartDelete(IFormCollection f)
        {
            var tid = f["ij"];

            int ffid = Convert.ToInt32(tid);
            string abc = User.FindFirst(ClaimTypes.Sid)?.Value;

            var upcart = db.Carts.FirstOrDefault(x => x.Userid == Convert.ToInt32(abc) && x.Id == ffid);
            if (upcart != null)
            {
                db.Carts.Remove(upcart);
                db.SaveChanges();
            }

            return RedirectToAction("ShowCart");
        }

        public IActionResult checkout()
        {
            string abc = User.FindFirst(ClaimTypes.Sid)?.Value;

            var cartt = db.Carts.Where(x => x.Userid.Equals
            (Convert.ToInt32(abc))).Include(x => x.Pro);


            ViewBag.Total = db.Carts.Where(x => x.Userid == Convert.ToInt32(abc)).Sum(x => x.Proprice);


            return View();

        }
        //---- Cart End ----//



        //---- CheckOut Start ----//
        [HttpPost]
        public IActionResult Checkout(IFormCollection f, Order o, OrderDetail ood)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // Get user ID
            string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;        // Get user email
            string userName = User.FindFirst(ClaimTypes.Name)?.Value;         // Get user name

            // Now pass the userName and userEmail to the View
            ViewBag.UserName = userName;
            ViewBag.UserEmail = userEmail;

            var name = f["cname"];
            var totalprice = f["tp"];
            var phone = f["cphone"];
            var address = f["caddress"];
            var date = f["cdate"];

            string abc = User.FindFirst(ClaimTypes.Sid)?.Value;

            o.Userid = Convert.ToInt32(abc);
            o.Date = DateTime.Now.ToString("dd-MM-yyyy"); // Fixed the date format
            o.CustomerName = name;
            o.TotalAmount = Convert.ToInt32(totalprice);
            o.PhoneNumber = phone;
            o.Address = address;
            db.Add(o);
            db.SaveChanges();

            List<Cart> carts = db.Carts
                                 .Where(cart => cart.Userid == Convert.ToInt32(abc))
                                 .Include(x => x.Pro)
                                 .ToList();

            foreach (var cartItem in carts)
            {
                // Create a new instance of OrderDetail for each cart item
                OrderDetail od = new OrderDetail
                {
                    Userid = Convert.ToInt32(abc),
                    OrderId = o.Id,
                    ProId = cartItem.Proid,
                    ProName = cartItem.Pro.Name,
                    Qty = cartItem.Qty,
                    Proprice = cartItem.Proprice,
                    Date = DateTime.Now.ToString("dd-MM-yyyy")
                };

                db.Add(od);

                var product = db.Products.FirstOrDefault(p => p.Id == cartItem.Proid);
                if (product != null)
                {
                    product.Quantity -= cartItem.Qty;
                    db.Products.Update(product);
                }

                db.Carts.Remove(cartItem);
            }

            db.SaveChanges();

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("pharmaease03@gmail.com", "cpeyztunyvumuupf")
            };

            MailMessage msg = new MailMessage("pharmaease03@gmail.com", name)
            {
                Subject = "Order Confirmed",
                Body = "Dear User, Your Order Details are as follows: Order ID: " + o.Id + " & Total Price: " + totalprice + ". Please be ready with this amount."
            };

            client.Send(msg);
            ViewBag.message = "Order placed & email sent successfully.";

            return View();
        }

        //---- CheckOut End ----//



        //---- Jobs ----//
        public IActionResult Jobs()

        {
            return View(db.Jobs.ToList());
        }

        [HttpGet]
        public IActionResult JobDetails(int Id)
        {
            var resultt = db.Jobs.Where(x => x.Id.Equals(Id));
            if (resultt == null)
            {
                return NotFound();
            }
            return View(resultt);
        }

        //---- Apply Jobs ----//
        [Authorize]
        [HttpPost]
        public IActionResult ApplyJob(int JobId, IFormFile Resume)
        {
            // Check if the user is logged in and has a valid user ID
            string userIdStr = User.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["ErrorMessage"] = "Please log in to apply for this job.";
                return RedirectToAction("Login", "Account");
            }

            int userId = Convert.ToInt32(userIdStr);

            // Check if the user has already applied for this job
            var existingApplication = db.ApplyJobs.FirstOrDefault(x => x.Jobid == JobId && x.Userid == userId);
            if (existingApplication != null)
            {
                TempData["ErrorMessage"] = "You have already applied for this job.";
                return RedirectToAction("JobDetails", new { Id = JobId });
            }

            // Handle the resume upload if a file is provided
            if (Resume == null || Resume.Length == 0)
            {
                TempData["ErrorMessage"] = "Please upload a valid resume.";
                return RedirectToAction("JobDetails", new { Id = JobId });
            }

            // Save the file (similar to your original code)
            string resumeFilePath = null;
            var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var filePath = Path.Combine(uploadDirectory, Resume.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Resume.CopyTo(stream);
            }

            resumeFilePath = "/uploads/" + Resume.FileName;

            // Create a new application record
            var newApplication = new ApplyJob
            {
                Jobid = JobId,
                Userid = userId,
                Status = "Pending",
                Resume = resumeFilePath
            };

            // Save the application to the database
            db.ApplyJobs.Add(newApplication);
            db.SaveChanges();

            TempData["SuccessMessage"] = "You have successfully applied for the job!";
            return RedirectToAction("JobDetails", new { Id = JobId });
        }

        //---- Apply Job End ----//



        //-------- Contact Start --------//
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(IFormCollection form)
        {
            try
            {
                // Extract form data using IFormCollection
                var firstName = form["fname"];
                var lastName = form["lname"];
                var email = form["email"];
                var phone = form["phone"];
                var message = form["message"];

                // Save the data to the database
                var contact = new Contact
                {
                    FName = firstName,
                    LName = lastName,
                    Email = email,
                    Number = phone,
                    Message = message
                };

                db.Contacts.Add(contact);
                db.SaveChanges();

                // Email configuration
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("pharmaease03@gmail.com", "cpeyztunyvumuupf")
                };

                // Prepare the email message
                MailMessage msg = new MailMessage("pharmaease03@gmail.com", "pharmaease03@gmail.com") // Replace with admin's email
                {
                    Subject = "New Contact Form Submission",
                    Body = $"Name: {firstName} {lastName}\nEmail: {email}\nPhone: {phone}\nMessage: {message}"
                };

                // Send the email
                client.Send(msg);

                ViewBag.Message = "Your message was sent successfully!";
                ViewBag.AlertType = "success"; // For success alert
            }
            catch (Exception ex)
            {
                ViewBag.Message = "There was an error sending your message. Please try again.";
                ViewBag.AlertType = "danger"; // For error alert
            }

            return View();
        }

        //-------- Contact End --------//


        //---- User Profile Start ----//
        //public IActionResult profile()
        //{

        //    // Retrieve OrderDetails from DB
        //    List<OrderDetail> orderDetail = db.OrderDetails.ToList();
        //    List<Order> order = db.Orders.ToList();
        //    List<Product> product = db.Products.ToList();


        //    string abc = User.FindFirst(ClaimTypes.Sid)?.Value; ;
        //    // Filter based on UserId. Order by date. Select and group by relevant columns.
        //    IEnumerable<PurchasesViewModel> purchases =
        //        from o in order
        //        join od in orderDetail on o.Id equals od.OrderId
        //        join p in product on od.ProId equals p.Id
        //        where o.Userid == Convert.ToInt32(abc)

        //        select new { o.Date, od.Id, p.Image, p.Name, p.Description, od.ProId } into y
        //        group y by new { y.Image, y.Name, y.Description, y.ProId } into grp
        //        select new PurchasesViewModel
        //        {

        //            ImageLink = grp.Key.Image,
        //            Name = grp.Key.Name,
        //            Quantity = grp.Count(),
        //            Description = grp.Key.Description,

        //            ProductId = (int)grp.Key.ProId
        //        };

        //    if (purchases.ToList().Count == 0) // If no purchases, send info to View to display "no past purchases"
        //    {
        //        ViewData["HavePastOrders"] = false;

        //        return View();
        //    }

        //    ViewData["HavePastOrders"] = true;

        //    return View(purchases.ToList());
        //}
        //---- User Profile End ----//



        //--- Search Start ----//
        [HttpPost]
        public IActionResult Search(string xyz)
        {

            // var query = from x in db.Visitors.Where(x => x.Id == xyz) select x;
            var resultt = db.Products.Where(x => x.Name.Contains(xyz));
            //var resultt = db.Visitors.Where(x => EF.Functions.Like(x.Namee, "a%"));
            return View("Index", resultt.ToList());

        }
        //---- Search End ----//
    }
}
