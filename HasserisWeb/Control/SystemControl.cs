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
        
        public SystemControl()
        {

            using (var db = new HasserisDbContext())
            {
                db.Employees.Add(new Employee("Jakob", "Østenkjær", "AdminPlus", 150, new ContactInfo("jallehansen17/gmail.com", "28943519"), new Address("Herningvej 5", "9220", "Aalborg", "Første dør")));
                db.SaveChanges();
            }

        }


    }
    
}
