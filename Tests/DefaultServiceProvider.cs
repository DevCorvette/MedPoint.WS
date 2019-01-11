using System;
using System.Collections.Generic;
using System.Text;
using MedPoint;
using MedPoint.Exceptions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Autofac.Extensions.DependencyInjection;

namespace Tests
{
    public class DefaultServiceProvider
    {
        private static readonly object ServiceProviderLock = new object();
        private static IServiceProvider _serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    lock (ServiceProviderLock)
                    {
                        _serviceProvider = _serviceProvider ?? ConstructServiceProvider();
                    }
                }

                return _serviceProvider;
            }
        }

        private static IServiceProvider ConstructServiceProvider()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                .ConfigureServices(services => services.AddAutofac())
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddJsonFile("appsettings.Test.json", optional: false, reloadOnChange: true);
                })
                .UseStartup<Startup>()
                .Build();

            return webHost.Services;
        }

        public static T GetService<T>()
        {
            var service = (T)ServiceProvider.GetService(typeof(T));
            if (service == null)
            {
                throw new ServiceNotFoundException($"Service {typeof(T)} not found");
            }
            return service;
        }
    }

    [Serializable]
    public class ServiceNotFoundException : MedPointException
    {
        public ServiceNotFoundException() { }
        public ServiceNotFoundException(string message) : base(message) { }
        public ServiceNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ServiceNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
