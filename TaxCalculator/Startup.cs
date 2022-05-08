using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TaxCalculator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Data.SeedData;
using TaxCalculator.Repository;
using TaxCalculator.Repository.IRepository;
using TaxCalculator.TaxRules;
using TaxCalculator.Models;

namespace TaxCalculator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaxCalculator", Version = "v1" });
            });
            services.AddDbContext<ApplicationDbContext>
                (options =>
                 {
                     options.UseInMemoryDatabase(Configuration.GetConnectionString("TaxDb"));
                     //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                 }
                );
            services.AddScoped<ICalculateTaxRepository, CalculateTaxRepository>();

            services.AddTransient<ICalculateTaxByRule, CalculateTaxByRuleOne>();
            services.AddTransient<ICalculateTaxByRule, CalculateTaxByRuleTwo>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaxCalculator v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            SeedData.SeedDataInMemoryDB(context);
        }
    }
}
