using DotNet_Project.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Security.Cryptography;

namespace DotNet_Project.Controllers
{
    public class CookiesController : Controller
    {
        PharmacyContext db = new PharmacyContext();

        // user or admin dono ka same ha /// 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login lg)
        {
            ViewBag.a = lg.Email;  // This will display the Email or Name for debugging
            ViewBag.c = lg.Password;

            ClaimsIdentity identity = null;
            bool isAuthenticated = false;

            string fpass = HashPassword(lg.Password);
            string namee = lg.Name;
            string email = lg.Email;
            string password = fpass;  // Hashed password

            // Check if the user exists with either Email or Name
            var res = db.Logins.FirstOrDefault(x => (x.Email == email || x.Name == namee) && x.Password == password);

            if (res != null)
            {
                // Create the identity for the user based on their RoleId
                if (res.RoleId == 1) // Admin
                {
                    identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Sid, res.Id.ToString()),
                new Claim(ClaimTypes.Name, res.Name),  // User's name
                new Claim(ClaimTypes.Email, res.Email),  // User's email
                new Claim(ClaimTypes.Role, "Admin")
            }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;
                }
                else if (res.RoleId == 2) // User
                {
                    identity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Sid, res.Id.ToString()),
                new Claim(ClaimTypes.Name, res.Name),
                new Claim(ClaimTypes.Email, res.Email),
                new Claim(ClaimTypes.Role, "User")
            }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;
                }

                // Handle redirection after successful authentication
                if (isAuthenticated)
                {
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    if (res.RoleId == 1)
                        return RedirectToAction("Index", "Admin"); // Admin-specific page

                    if (res.RoleId == 2)
                        return RedirectToAction("Index", "User"); // User-specific page
                }
            }
            else
            {
                return Content("Wrong email/name or password");
            }

            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // ye user ka register ha //////////////////

        [HttpGet]
        public IActionResult Adduser()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Adduser(Login lg)
        {
            if (ModelState.IsValid)
            {
                var checkemail = db.Logins.Where(x =>  x.Email == lg.Email);
                if (checkemail.Count() == 0)
                {
                    lg.RoleId = 2;

                    lg.Password = lg.Password;
                    db.Logins.Add(lg);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Cookies");

                }
                else
                {
                    ViewBag.msg = "Email or Username already registered, please provide another email";
                }


            }
            return View();



        }



        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }




    }
}
