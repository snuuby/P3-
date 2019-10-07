using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HasserisWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //class test with placeholder information and values
            ContactInfo cont_1 = new ContactInfo("andper11@gmail.com", "69420");
            Address add_1 = new Address("Vimmervej 11", "9920", "Aalborg", "Hello World");
            Customer cust_1 = new Private("Anders", "Petersen", add_1, cont_1);
            Calendar cal = new Calendar();
            Appointment flyt_1 = new Delivery("Anders", "Flytning", cust_1, add_1, 233.1, DateTime.Today, "Kom hurtigst", "44440", "???", 5);
            //Running test to confirm duration of appointment is correct.
            flyt_1.BeginAppointment();
            Thread.Sleep(1000);
            cal.AddAppointment(flyt_1);
            cal.CheckToday();
            flyt_1.EndAppointment();
            Console.WriteLine($"Appointment lasted {flyt_1.appointmentDuration.Hours} hours, {flyt_1.appointmentDuration.Minutes} minutes and {flyt_1.appointmentDuration.Seconds} seconds. ");
        }
    }
}
        /*
            CreateWebHostBuilder(args).Build().Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
*/