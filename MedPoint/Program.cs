﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Autofac.Extensions.DependencyInjection;

namespace MedPoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    //IHostingEnvironment env = builderContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .UseStartup<Startup>()
                .Build();
    }
}
