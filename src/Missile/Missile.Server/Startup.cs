using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Missile.Core;
using Swashbuckle.AspNetCore.Swagger;

namespace Missile.Server
{
    public class Startup
    {
        private string pluginPath = @"C:\Users\master\Documents\computing\projects\missile\src\Missile";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Missile API", Version = "v1" });
            });
                                             
            IContainer built = BuildContainer(services);
            
            return new AutofacServiceProvider(built);
        }

        private IContainer BuildContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.Populate(services);

            var pluginFiles = GetPluginFiles();
            pluginFiles.ForEach(x => Assembly.LoadFile(x));

            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.IsAssignableTo<IPlugin>())
                .As<IPlugin>();
            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.IsAssignableTo<IService>())
                .As<IService>();
            
            return builder.Build();
        }

        private List<string> GetPluginFiles()
        {
            return new List<string>()
            {
                 @"C:\Users\master\Documents\computing\projects\missile\src\Missile\Missile.GooglePlugin\bin\Debug\netcoreapp2.0\Missile.GooglePlugin.dll",
                 @"C:\Users\master\Documents\computing\projects\missile\src\Missile\Missile.EverythingPlugin\bin\Debug\netcoreapp2.0\Missile.EverythingPlugin.dll"
            };
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();
                                                                                                                   
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Missile API V1");
            });
        }
    }
}
