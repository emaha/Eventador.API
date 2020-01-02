using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net;

namespace Eventador
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                //.UseSerilog()
                .UseKestrel(
                    options =>
                    {
                        options.Listen(IPAddress.Loopback, 5020);
                    })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build();
    }
}