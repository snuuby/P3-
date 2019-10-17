using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace HasserisWeb
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        [Route("verify")]
        [HttpPost]
        public bool VerifyUserLogin(string json)
        {
            dynamic tempstring = JsonConvert.DeserializeObject(json);
            string username = "";
            string password = "";
            foreach (var str in tempstring)
            {
                username = str.username;
                password = str.password;
            }

            try {
    
            Employee temp = HasserisDbContext.VerifyPassword(password, username);
            }
            catch(Exception e) {
                return false;
            }
            return true;
        }
    }
}