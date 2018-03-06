using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SoltimateWeb
{
    /// <summary>
    /// Program wrapper class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entrance point for WebApplication.
        /// </summary>
        /// <param name="args">Commandline arguments.</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Build web host to run the application.
        /// </summary>
        /// <param name="args">Arguments for creating the host.</param>
        /// <returns>Instance of webhost.</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
