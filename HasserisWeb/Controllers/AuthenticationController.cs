using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Web.Http;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace HasserisWeb
{
    public class ReturnObjects
    {
        public Employee user;
        public string access_token;
        public string error = "";
    }
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("auth")]
    public class AuthenticationController : ControllerBase
    {

        public ReturnObjects returnObjects = new ReturnObjects();

        [Microsoft.AspNetCore.Mvc.Route("verify")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public string Get(dynamic json)
        {    
            dynamic tempstring = JsonConvert.DeserializeObject(json.ToString());
            string username = tempstring.name;
            string password = tempstring.pass;


            if (CheckUser(username, password))
            {
                returnObjects.access_token = GenerateToken(username);
                HasserisDbContext.SetAccessToken(returnObjects.access_token, returnObjects.user.id);
            }

            return JsonConvert.SerializeObject(returnObjects);

        }



        public string GenerateToken(string username, int expireMinutes = 20)
        {
            var hmac = new HMACSHA256();
            var key = Convert.ToBase64String(hmac.Key);
            var symmetricKey = Convert.FromBase64String(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public bool CheckUser(string username, string password)
        {
            // should check in the database


            try
            {

                returnObjects.user = HasserisDbContext.VerifyPassword(password, username);
                returnObjects.user.type = returnObjects.user.type.ToLower();
                returnObjects.user.profilePhoto = HasserisDbContext.GetEmployeeProfileImage(returnObjects.user.userName);
                returnObjects.user.contactInfo.email = returnObjects.user.contactInfo.email.Replace('/', '@');
            }
            catch (Exception e)
            {
                returnObjects.user = null;
                returnObjects.error = "Brugernavn eller password er forkert";
                return false;

            }
            return true;
        }
        [Microsoft.AspNetCore.Mvc.Route("AccessToken")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public string GetAccessToken(dynamic json) 
        {

            dynamic tempstring = JsonConvert.DeserializeObject(json.ToString());
            string token = tempstring.access_token;
            try
            {
                returnObjects.user = HasserisDbContext.GetAccessTokenUser(token);
                returnObjects.user.profilePhoto = HasserisDbContext.GetEmployeeProfileImage(returnObjects.user.userName);
                returnObjects.user.type = returnObjects.user.type.ToLower();
                returnObjects.user.contactInfo.email = returnObjects.user.contactInfo.email.Replace('/', '@');

            }
            catch(Exception)
            {
                return null;
            }
            returnObjects.access_token = token;
            return JsonConvert.SerializeObject(returnObjects);
        } 
        /*
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
            
        } */
    }
}