using AMS_Models.Models;
using ApartmentManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApartmentManagementContext AMCcontext;
        public HomeController(ILogger<HomeController> logger, ApartmentManagementContext ams)
        {
            _logger = logger;
            AMCcontext = ams;
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
        public IActionResult LoginPage(string msg)
        {
            ViewBag.mess = msg;
            Response.Cookies.Delete("mycookie");
            return View();
        }
        [HttpPost]
        public IActionResult LoginPage(string Aname, string Password)
        {
            AdminLogin usr = AMCcontext.AdminLogins.Where(user => user.Aname == Aname && user.Password == Password).FirstOrDefault();
            //
            //AdminLogin usr = _dbcontext.AdminLogins.Where(v => v.Aname == AdminName && v.Password == PassWord).FirstOrDefault();

            //AdminLogin usr = userdb().Where(v => v.Aname == AdminName && v.Password == PassWord).FirstOrDefault();
            if (usr != null)
            {
                var token = Createtoken();
                savetoken(token);

                return RedirectToAction("AdminMenuLandingPage");
            }
            return View("LoginPage", "Username or Password is incorrect");
        }

        private void savetoken(object token)
        {
            var cookdet = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddHours(2),
            };
            Response.Cookies.Append("mycookie", (string)token, cookdet);
        }

        private object Createtoken()
        {
            var Skey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrst"));
            var credentials = new SigningCredentials(Skey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(

                issuer: "abc",
                audience: "abc",
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials


                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IActionResult LoginPageForCustomer(string msg)
        {
            ViewBag.mess = msg;
            Response.Cookies.Delete("mycookie");
            return View();
        }
        [HttpPost]
        public IActionResult LoginPageForCustomer(string UserName, string Password)
        {
            UserRegistration usr = AMCcontext.UserRegistrations.Where(user => user.UserName == UserName && user.Password == Password).FirstOrDefault();

            if (usr != null)
            {
                var token = Createtoken();
                savetoken(token);
                HttpContext.Session.SetInt32("CustomerId", usr.Uid);
                return RedirectToAction("ShowAll");
            }


            return View("LoginPageForCustomer", "Username or Password is incorrect");
        }
    }
}
