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
            /*
            using (var db = new HasserisDbContext())
            {
                db.Database.EnsureCreated();
            }
            */
            CreateWebHostBuilder(args).Build().Run();
        }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
    
    }
}


