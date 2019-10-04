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
            MakeEmployee();
            MakeCustomer();
            LoadEmployee();
            LoadCustomer();
        }
        public void MakeEmployee()
        {



        }
        public void MakeCustomer()
        {

        }
        public void LoadEmployee()
        {
            dynamic employeestest = HasserisDbContext.LoadElement<Employee>(new Employee());
            List<Employee> employees = (List<Employee>)employeestest;
        }
        public void LoadCustomer()
        {
            dynamic customertest = HasserisDbContext.LoadElement<Customer>(new Customer());
            List<Customer> customers = (List<Customer>)customertest;

            
        }

    }
}
