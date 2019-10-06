using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
namespace HasserisWeb
{
    public class DatabaseTester
    {
        public DatabaseTester()
        {
            Delivery test = new Delivery("test", "Delivery", 10000,
                new PrivateCustomer("jakob", "hansen", "Private",
                    new Address("myrdal", "2", "aalborg", "testnote"),
                    new ContactInfo("hansen@gmail", "2233")),
                new Address("myrdal", "2", "aalborg", "testnote"), 1000, new DateTime(2019, 3, 12), "testnote", "22331133", "Foam", 2);

            test = HasserisDbContext.SaveElementToDatabase<PrivateCustomer>(test);
            Debug.WriteLine(test.name);
        }

    }
}
