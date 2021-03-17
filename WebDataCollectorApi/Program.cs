using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WebDataCollectorApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                var env = webBuilder.GetSetting("environment");
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{env}.json", true, true)
                    .AddEnvironmentVariables()
                    .Build();

                webBuilder.UseUrls(config["HostUrl"]);
                webBuilder.UseStartup<Startup>();
            });
        }
    }
}