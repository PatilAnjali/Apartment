using AMS_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApartmentManagementSystem.Controllers.AMS_UI_Controllers
{
    public class UserRegistrationUIController : Controller
    {
       /* public IActionResult Index()
        {
            return View();
        }*/
       public IActionResult showuserdetails()
        {
            IEnumerable<UserRegistration> usr = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("https://localhost:44389/api/");
            var us = cln.GetAsync("UserRegistrationAPI");
            us.Wait();
            var usd = us.Result;
            if(usd.IsSuccessStatusCode)
            {
                var data = usd.Content.ReadAsAsync<IList<UserRegistration>>();
                data.Wait();
                usr = data.Result;
            }
            return View(usr);
        }

        [HttpGet]
        public IActionResult UserDetails(string Uid)
        {
            UserRegistration flight = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("https://localhost:44389/api/");
            var getflight = cln.GetAsync("UserRegistrationAPI/" + Uid);
            getflight.Wait();

            var res = getflight.Result;
            if (res.IsSuccessStatusCode)
            {
                var dat = res.Content.ReadAsAsync<UserRegistration>();
                dat.Wait();
                flight = dat.Result;
            }
            return View(flight);
        }
        public IActionResult insertuser()
        {
            IEnumerable<UserRegistration> docs = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("https://localhost:44389/api/");
            var getuser = cln.GetAsync("UserRegistrationAPI");
            getuser.Wait();
            var result = getuser.Result;
            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsAsync<IEnumerable<UserRegistration>>();
                data.Wait();
                docs = data.Result;

            }
            return View();
        }
        [HttpPost]
        public IActionResult insertuser(UserRegistration userdet)
        {
            //send_email(userdet.UserName, userdet.EmailId);// to get the details from the user and pass it on to the send_email method
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("https://localhost:44389/api/");
            var post = cln.PostAsJsonAsync<UserRegistration>("UserRegistrationAPI/", userdet);
            post.Wait();
            var res = post.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("showuserdetails", "UserRegistrationUI");
            }
            return View(userdet);
        }


        [HttpGet]
        public IActionResult EditUser(string Uid)
        {
            UserRegistration flight = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("https://localhost:44389/api/");
            var getflight = cln.GetAsync("UserRegistrationAPI/" + Uid);
            getflight.Wait();

            var res = getflight.Result;
            if (res.IsSuccessStatusCode)
            {
                var dat = res.Content.ReadAsAsync<UserRegistration>();
                dat.Wait();
                flight = dat.Result;
            }
            return View(flight);
        }
        [HttpPost]
        public IActionResult EditUser(UserRegistration user)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("https://localhost:44389/api/");
            var updateflight = cln.PutAsJsonAsync<UserRegistration>("userAPI", user);
            updateflight.Wait();

            var res = updateflight.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("showuserdetails");
            }
            return RedirectToAction("Index");
        }


        /*[HttpGet]
        public IActionResult DeleteUser(int Uid)
        {
            UserRegistration userdet = null;
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("https://localhost:44389/api/");
            var getflight = cln.GetAsync("UserRegistrationAPI/" + Uid);
            getflight.Wait();

            var res = getflight.Result;
            if (res.IsSuccessStatusCode)
            {
                var dat = res.Content.ReadAsAsync<UserRegistration>();
                dat.Wait();
                userdet = dat.Result;
            }
            return View(userdet);
        }
        [HttpPost]
        public IActionResult DeleteUser(UserRegistration user)
        {
            var cln = new HttpClient();
            cln.BaseAddress = new Uri("https://localhost:44389/api/");
            var del = cln.DeleteAsync("UserRegistrationAPI/Del/" + user.Uid);
            del.Wait();
            var res = del.Result;
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction("showuserdetails");
            }
            return RedirectToAction("Index");
        }*/

    }
}
