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

        
        [Microsoft.AspNetCore.Mvc.Route("uploadImage")]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public void uploadProfileImage(dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            string tempBase64 = temp.base64URL;
            string tempUsername = temp.username;
            string filePath = "..//HasserisWeb/ClientApp/public/assets/images/avatars/" + tempUsername;
            dynamic decodedStr = Base64UrlEncoder.Decode(tempBase64);
            using (var imageFile = new System.IO.FileStream(filePath, FileMode.Create))
            {
                imageFile.Write(decodedStr, 0, decodedStr.Length);
                imageFile.Flush();
            }
        }

    }

}
