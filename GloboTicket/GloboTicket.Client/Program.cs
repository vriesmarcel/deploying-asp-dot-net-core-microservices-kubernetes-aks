using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace GloboTicket.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WriteBase64PfxFileFromEnvironmentToDisk();
            CreateHostBuilder(args).Build().Run();
        }

        private static void WriteBase64PfxFileFromEnvironmentToDisk()
        {
            var base64pfxfile = Environment.GetEnvironmentVariable("base64pfxfile");
            if (!string.IsNullOrWhiteSpace(base64pfxfile))
            {
                Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path");
                var filename = Path.Combine(Directory.GetCurrentDirectory(), "certfile.pfx");
                File.WriteAllBytes(filename, Convert.FromBase64String(base64pfxfile));
                Environment.SetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path", filename);
            }
        }
            public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
