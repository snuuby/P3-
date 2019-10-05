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
        public static void DeleteElement<T>(dynamic element)
        {
            if (element is Employee)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Employee employee = (Employee)element;
                    cnn.Execute("DELETE FROM Employees WHERE ID = (@id)", employee);
                }
            }
            else if (element is Appointment) 
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Appointment appointment = (Appointment)element;
                    cnn.Execute("DELETE FROM Appointments WHERE ID = (@id)", appointment);
                }
            }
            else if (element is Customer)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Customer customer = (Customer)element;
                    cnn.Execute("DELETE FROM Customers WHERE ID = (@id)", customer);
                }
            }
        }
        public static void SaveElement<T>(dynamic element)
        {
            if (element is Employee)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Employee employee = (Employee)element;
                    cnn.Execute("INSERT INTO Employees (FirstName, LastName) Values (@firstName, @lastName)", employee);
                }
            }
            else if (element is Appointment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Appointment appointment = (Appointment)element;
                    cnn.Execute("INSERT INTO Appointments (FirstName, LastName) Values (@firstName, @lastName)", appointment);
                }
            }
            else if (element is Customer)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    Customer customer = (Customer)element;
                    cnn.Execute("INSERT INTO Customers (FirstName, LastName) Values (@firstName, @lastName)", customer);
                }
            }
            else
            {
                throw new Exception("Can only remove objects from database that are in the database");
            }
        }
        public static dynamic LoadElement<T>(dynamic element)
        {
            if (element is Employee)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    var output = cnn.Query<Employee>("select * from Employees", new DynamicParameters());
                    dynamic realOutput = output.ToList();
                    return realOutput;
                }
            }
            else if (element is Appointment)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    var output = cnn.Query<Appointment>("select * from Appointments", new DynamicParameters());
                    dynamic realOutput = output.ToList();
                    return realOutput;
                }
            }
            else if (element is Customer)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    var output = cnn.Query<Customer>("select * from Customers", new DynamicParameters());
                    dynamic realOutput = output.ToList();
                    return realOutput;
                }
            }
            else
            {
                throw new Exception("Can only remove objects from database that are in the database");
            }
        }
        //Example call: ModifyElement(employee, new string[] {"Wage", "Name"});
        public static void ModifyElement<T>(dynamic element, string[] toModify)
        {

            if (element is Employee)
            {
                updateEmployee((Employee)element, toModify);
            }
            else if (element is Appointment)
            {

            }
            else if (element is Equipment)
            {

            }
        }
        private static void updateEmployee(Employee employee, string[] toModify)
        {
            string sqlStatement = null;
            foreach (string property in toModify)
            {
                using (IDbConnection cnn = new SQLiteConnection(GetDefaultConnectionString()))
                {
                    switch (property)
                    {
                        case "Wage":
                            sqlStatement = "UPDATE Employees SET " + property + " = '" + employee.wage + "'";
                            cnn.Execute(sqlStatement);
                            break;
                        case "Firstname":
                            sqlStatement = "UPDATE Employees SET " + property + " = '" + employee.firstName + "'";
                            cnn.Execute(sqlStatement);
                            break;
                        case "Lastname":
                            sqlStatement = "UPDATE Employees SET " + property + " = '" + employee.lastName + "'";
                            cnn.Execute(sqlStatement);
                            break;
                        case "Appointment":

                        default:
                            throw new Exception("Can't modify non-existing property");
                    }

                }
            }
        }
        private static string GetDefaultConnectionString()
        {

            return Startup.ConnectionString;
        }

    }
}
