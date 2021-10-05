using AMS_BAL;
using AMS_Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentManagementSystem.Controllers.AMS_API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationAPI : ControllerBase
    {
        private BAL_UserRegistration userbal;
        public UserRegistrationAPI(BAL_UserRegistration user)
        {
            userbal = user;
        }
        [HttpGet]
        public IActionResult getalluser()
        {
            return Ok(userbal.GetUserdetails());

        }
        [HttpGet("{Uid}")]
        public IActionResult getuserbyid(int Uid)
        {
            return Ok(userbal.GetUserdetailbyID(Uid));
        }
        [HttpPost]
        public IActionResult insertuserdetails(UserRegistration data1)
        {
            return Ok(userbal.AddCustomer(data1));
        }
        [HttpPut]
        public IActionResult updateuserdetails(UserRegistration data)
        {
            return Ok(userbal.UpdateCustomer(data));
        }
       /* [HttpDelete("{Uid}")]
        public void DeleteFlight(string Uid)
        {
            userbal.DeleteCustomer(Uid);
        }*/
    }
}