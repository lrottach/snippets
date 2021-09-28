using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace Snippets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
                //.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Verbose)
                //.MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Verbose)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                //.WriteTo.Debug() //schreibt in das Visual Studio Output-Fenster
                .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "\\logfile.log", rollingInterval: RollingInterval.Month, rollOnFileSizeLimit: true)
                .CreateLogger();

            try
            {
                Log.Information("Server is starting...");
                CreateHostBuilder(args).Build().Run();
                Log.Information("Server was stopped.");
            }
            catch(Exception exc)
            {
                Log.Fatal(exc, exc.Message);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}