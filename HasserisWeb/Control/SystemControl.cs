using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography;

namespace HasserisWeb
{
    public class SystemControl
    {

        public static Calendar calendar = new Calendar("Hasseris Calendar");
        public SystemControl()
        {

            DatabaseTester test = new DatabaseTester();
            Employee jakob;
            try
            {
                jakob = VerifyPassword("jakob17", "snuuby");
                Debug.WriteLine(jakob.id);
                //Do something with Jakob here
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("Wrong password");
            }
            catch (Exception)
            {
                Debug.WriteLine("Likely wrong username");
            }

        }
        public Employee VerifyPassword(string password, string username)
        {
            
            /* Fetch the stored value */
            string savedPasswordHash = HasserisDbContext.LoadEmployeeHashPassword(username);
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new UnauthorizedAccessException();

            return HasserisDbContext.LoadEmployeeFromHashPassword(savedPasswordHash);
        }

    }
}
