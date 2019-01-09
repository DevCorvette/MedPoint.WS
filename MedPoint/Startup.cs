using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MedPoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            return ConfigureAutofac(services);
        }

        private IServiceProvider ConfigureAutofac(IServiceCollection services)
        {
            var module = new ConfigurationModule(Configuration);
            var builder = new ContainerBuilder();
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterModule(module);
            builder.RegisterInstance(Configuration).As<IConfiguration>().SingleInstance();
            builder.Populate(services);

            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Namespace.StartsWith("MedPoint"))
                .AsImplementedInterfaces();

            return builder.Build().Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
