using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace SelfHostService
{
    class Program
    {
        static void Main(string[] args)
        {
            //string baseAddress = "http://127.0.0.1:9033/";

            IWebHost host = new WebHostBuilder().UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel().UseStartup<Startup>().ConfigureLogging(log =>
                {
                  
                }).ConfigureKestrel((context, options) =>
                {
                    
                }).Build();

            host.Run();
            Console.ReadLine();
        }
    }
}
