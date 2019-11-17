using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;

namespace HasserisWeb
{
    public class Employee
    {
        public string PhotoPath { get; set; }
        [Required]
        public string Firstname { get;  set; }
        [Required]
        public string Lastname { get;  set; }
        public string Username { get; set; }
        [JsonIgnore]
        public ICollection<TaskAssignedEmployees> taskAssignedEmployees { get; set; } = new List<TaskAssignedEmployees>();
        public ICollection<InspectionAssignedEmployees> Inspections { get; set; } = new List<InspectionAssignedEmployees>();

        public string Hashcode { get; set; }
        public bool IsAvailable { get; private set; } = true;
        public string AccessToken { get; set; }
        public ContactInfo ContactInfo { get; set; }
        [Required]
        public double Wage { get; private set; }
        public int ID { get; set; }
        [Required]
        public Address Address { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Employed { get; set; }
        public Employee()
        {

        }

        public Employee(string Firstname, string Lastname, string Type, double Wage, ContactInfo ContactInfo, Address Address)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.Wage = Wage;
            this.ContactInfo = ContactInfo;
            this.Address = Address;
            this.Type = Type;
            this.Employed = "employed";
            this.PhotoPath = "assets/images/avatars/profile.jpg";
        }
        public void AddLoginInfo(string username, string password)
        {
            this.Username = username;
            this.Hashcode = CalculateHash(password);
        }
        private string CalculateHash(string tempPassword)
        {
            //Step 1: Create the salt value with cryptographic PRNG
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            //Step 2: Create the Rfc2898DeriveBytes and get the hash value:

            var pbkdf2 = new Rfc2898DeriveBytes(tempPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            //Step 3: Combine the salt and password bytes for later use
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //Step 4: Turn the combined salt+hash into a string for storage
            return Convert.ToBase64String(hashBytes);
        }
    }
}
