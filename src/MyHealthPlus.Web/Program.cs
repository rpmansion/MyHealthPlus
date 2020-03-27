using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MyHealthPlus.Web.Extensions;

namespace MyHealthPlus.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(x => x.AddServerHeader = false);
                    webBuilder.UseStartup<Startup>();
                });
    }
}