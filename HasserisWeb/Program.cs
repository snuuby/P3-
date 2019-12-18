using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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


