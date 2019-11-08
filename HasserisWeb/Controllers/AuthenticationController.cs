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
using Microsoft.EntityFrameworkCore;


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
        public HasserisDbContext database;
        public AuthenticationController(HasserisDbContext sc)
        {
            database = sc;
        }
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

                    var employee = database.Employees
                                    .Include(contact => contact.ContactInfo)
                                    .Include(address => address.Address)
                                    .FirstOrDefault(e => e.Username == username); ;
                    employee.AccessToken = returnObjects.access_token;
                    database.Employees.Update(employee);
                    database.SaveChanges();
                
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
 

                returnObjects.user = VerifyPassword(password, username);
                returnObjects.user.Type = returnObjects.user.Type.ToLower();

                
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
                returnObjects.user = database.Employees
                                    .Include(contact => contact.ContactInfo)
                                    .Include(address => address.Address)
                                    .FirstOrDefault(e => e.AccessToken == token);
                returnObjects.user.Type = returnObjects.user.Type.ToLower();

            }
            catch(Exception)
            {
                return null;
            }
            returnObjects.access_token = token;
            return JsonConvert.SerializeObject(returnObjects);
        }
        public Employee VerifyPassword(string password, string username)
        {

                var employee = database.Employees
                    .Include(contact => contact.ContactInfo)
                    .Include(address => address.Address)
                    .FirstOrDefault(e => e.Username == username);

                string savedPasswordHash = employee.Hashcode;
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);
                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                        throw new UnauthorizedAccessException();
                return employee;
            
            
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