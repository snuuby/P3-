using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
namespace HasserisWeb
{
    public class SystemControl
    {

        public static Calendar calendar = new Calendar("Hasseris Calendar");
        public SystemControl()
        {

            DatabaseTester test = new DatabaseTester();

        }

    }
}
