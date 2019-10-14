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
            try
            {
                Employee jakob = HasserisDbContext.VerifyPassword("Jakob17", "Snuuby");
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


    }
}
