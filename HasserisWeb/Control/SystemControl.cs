using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
namespace HasserisWeb
{
    public class SystemControl
    {
        public static List<Appointment> appointments = new List<Appointment>();
        public static List<Equipment> equipment = new List<Equipment>();
        public static List<Employee> employees = new List<Employee>();
        public static List<Customer> customers = new List<Customer>();
        public static Calendar calendar = new Calendar("Hasseris Calendar");
        public SystemControl()
        {

            DatabaseTester test = new DatabaseTester();

        }

    }
}
