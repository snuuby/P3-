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
using System.IO;

namespace HasserisWeb
{

    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("images")]
    public class ImageController : ControllerBase
    {

        public HasserisDbContext database;
        public ImageController(HasserisDbContext sc)
        {
            database = sc;
        }
        [Microsoft.AspNetCore.Mvc.Route("uploadImage")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public string uploadProfileImage(dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            string tempBase64 = temp.base64URL;
            string tempUsername = temp.username;
            string tempType = temp.type;
            string tempSubType = tempType.Substring(6);
            string filePath = "..//HasserisWeb/ClientApp/public/assets/images/avatars/" + tempUsername + "." + tempSubType;
            byte[] bytearray = Base64UrlEncoder.DecodeBytes(tempBase64);
            int arrayCount = bytearray.Length;
            using (var imageFile = new System.IO.FileStream(filePath, FileMode.Create))
            {
                imageFile.Write(bytearray, 0, arrayCount);
                imageFile.Flush();
            }
            string newFilePath = "assets/images/avatars/" + tempUsername + "." + tempSubType;

                var employee = database.Employees.FirstOrDefault(e => e.Username == tempUsername);
                employee.PhotoPath = newFilePath;
                database.SaveChanges();

            
            return newFilePath;
        }

    }

}
