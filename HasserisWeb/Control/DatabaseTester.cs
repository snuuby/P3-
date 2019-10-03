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
            LoadEmployee();
        }
        public void MakeEmployee()
        {
            Employee testEmployee = new Employee();
            testEmployee.firstName = "Lars";
            testEmployee.lastName = "Eriksen";

            HasserisDbContext.SaveEmployee(testEmployee);
        }
        public void LoadEmployee()
        {
            List<Employee> employees = HasserisDbContext.LoadEmployees();
            foreach(Employee employee in employees)
            {
                Debug.WriteLine(employee.firstName + " " + employee.lastName);
            }
        }
    }
}
