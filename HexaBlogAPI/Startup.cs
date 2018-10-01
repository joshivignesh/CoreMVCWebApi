using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HexaBlogAPI.Infrastructure;
using HexaBlogAPI.Models;
using HexaBlogAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HexaBlogAPI
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
            services.AddCors();

            services.AddCors(options =>
            {
                //////options.AddDefaultPolicy(policy =>
                //////{
                //////    policy.AllowAnyOrigin()
                //////    .AllowAnyMethod()
                //////    .AllowAnyHeader();
                //////});

                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });

            });


            services.AddDbContext<BlogsContext>(options =>

            {
                options.UseSqlServer(Configuration.GetValue<string>("ConnectionString"));
            });

            services.AddScoped<IRepository<Blog>, Reposirory<Blog>>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");
            //app.UseCors(options =>
            {

                //////options.AllowAnyOrigin()
                //////.AllowAnyMethod()
                //////.AllowAnyHeader();

                //////options.WithOrigins("eff.cpm", "xyz.com")
                //////.WithMethods("GET")
                //////.WithHeaders("Content-type", "Content-Length", "Authorization");

            ////// });

            app.UseMvc();
        }
    }
}
