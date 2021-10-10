using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lcw_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                         
                        .WriteTo.File(
                                Path.Combine("log", "log.txt"),
                                rollingInterval: RollingInterval.Day,
                                rollOnFileSizeLimit: true,
                                buffered: false,
                                fileSizeLimitBytes: 21474836,
                                retainedFileCountLimit: 10
                                )
                        .CreateLogger();

            try
            {
                Log.Logger.Information("!!!!System UP!!!!!");
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                Log.Logger.Information("!!!!System DOWN!!!!!");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            }).UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
