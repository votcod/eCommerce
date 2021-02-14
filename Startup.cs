using eCommerce.Models;
using eCommerce.Models.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            string connection = Configuration["Data:ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DataContext>(r => r.UseSqlServer(connection));
            services.AddTransient<IDataRepository<Category, Category>, CategoryRepository>();
            services.AddTransient<IDataRepository<Product, ProductEditViewModel>, ProductRepository>();
            services.AddTransient<IDataAction, ProductRepository>();
            services.AddTransient<IDataRepository<Order, Order>, OrderRepository>();
            services.AddLogging();
            services.AddHostedService<CurrencyService>();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { controller = "Product", action = "List" }
                    );
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Product", action = "List" }
                    );
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
        }
    }
}
