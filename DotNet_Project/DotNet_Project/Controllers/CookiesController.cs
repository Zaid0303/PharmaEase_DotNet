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
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;

            //string fpass = HashPassword(lg.Password);

            var res = db.Logins.FirstOrDefault(x =>  x.Email == lg.Email && x.Password == lg.Password);

            if (res != null)
            {
                if (res.RoleId == 1)
                {

                    //Create the identity for the user
                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Sid, res.Id.ToString()),
                    new Claim(ClaimTypes.Email, lg.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;
                }

                if (res.RoleId == 2)
                {
                    //Create the identity for the user
                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Sid, res.Id.ToString()),
                    new Claim(ClaimTypes.Email, lg.Email),
                    new Claim(ClaimTypes.Role, "User")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;
                }


                if (isAuthenticated && res.RoleId == 1)
                {
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                if (isAuthenticated && res.RoleId == 2)
                {
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "User");
                }


            }

            else
            {
                return Content("Wrong email and password");

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

                    lg.Password = HashPassword(lg.Password);
                    db.Logins.Add(lg);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");

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
