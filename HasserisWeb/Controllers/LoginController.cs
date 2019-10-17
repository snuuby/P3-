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
        public bool VerifyUserLogin(dynamic json)
        {
            dynamic tempstring = JsonConvert.DeserializeObject(json.ToString());
            string username = tempstring.username;
            string password = tempstring.password;


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