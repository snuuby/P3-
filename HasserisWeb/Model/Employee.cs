using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace HasserisWeb
{
    public class Employee
    {
        public string firstName { get;  set; }
        public string lastName { get;  set; }
        public string userName { get; set; }
        public string hashCode { get; set; }
        public bool isAvailable { get; private set; }
        public ContactInfo contactInfo { get; set; }
        public double wage { get; private set; }
        public int id { get; set; }
        public Address address { get; set; }
        public string type { get; set; }

        // Test constructor Cholle
        public Employee()
        {
            
        }
        
        public Employee(string fName, string lName, string type, double pWage, ContactInfo contactInfo, Address address)
        {
            this.firstName = fName;
            this.lastName = lName;
            this.wage = pWage;
            this.contactInfo = contactInfo;
            this.address = address;
            this.type = type;
        }
        public void AddLoginInfo(string username, string password)
        {
            this.userName = username;
            this.hashCode = CalculateHash(password);
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