using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.EntityFrameworkCore;


namespace HasserisWeb
{
    public class HasserisDbContext : DbContext
    {
        public static List<Employee> LoadEmployees()
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                var output = cnn.Query<Employee>("select * from Employees", new DynamicParameters());
                return output.ToList();
            }
        }
        public static List<Customer> LoadCustomers()
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                var output = cnn.Query<Customer>("select * from Customers", new DynamicParameters());
                return output.ToList();
            }
        }
        public static List<Appointment> LoadAppointments()
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                var output = cnn.Query<Appointment>("select * from Appointments", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveEmployee(Employee employee)
        {
            using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
            {
                cnn.Execute("INSERT INTO Employees (FirstName, LastName) Values (@firstName, @lastName)", employee);
                
            }
        }


        private static string GetDefaultConnectionString()
        {

            return Startup.ConnectionString;
        }

    }
}
