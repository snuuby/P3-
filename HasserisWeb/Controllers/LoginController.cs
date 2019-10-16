using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace HasserisWeb
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        [Route("verify")]
        [HttpPost]
        public bool VerifyUserLogin(string username, string password)
        {
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