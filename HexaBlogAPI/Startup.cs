using HexaBlogAPI.Filters;
using HexaBlogAPI.Infrastructure;
using HexaBlogAPI.Models;
using HexaBlogAPI.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

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

                ////options.AddPolicy("MyPolicy", policy =>
                ////{
                ////    policy.AllowAnyOrigin()
                ////    .AllowAnyMethod()
                ////    .AllowAnyHeader();
                ////});

            });


            services.AddDbContext<BlogsContext>(options =>

            {
                options.UseSqlServer(Configuration.GetValue<string>("ConnectionString"));
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = "https://twitter.com/spboyer"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Set the comments path for the Swagger JSON and UI.
                //c.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<IRepository<Blog>, Reposirory<Blog>>();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ApiExceptionFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler(options =>
                {
                    options.Run(async (context) =>
                    {
                        ////var msg = "Some error occured in the server";
                        ////await context.Response.WriteAsync(msg);
                        //context.Response.WriteAsync(msg);
                    });
                });
            }

            


            //app.UseStatusCodePages();
            //app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
            //app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

            app.UseStaticFiles();

            app.UseCors("MyPolicy");


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            

            //////////app.UseswaggerUI(Options =>
            //////////{
            //////////    Options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Blog Swagger");
            //////////}



            ////////app.UseSwagger();
            ////////app.UseswaggerUI(Options =>
            ////////{
            ////////    Options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Blog Swagger");
            ////////}
            //app.UseCors(options =>
            //{

            //////options.AllowAnyOrigin()
            //////.AllowAnyMethod()
            //////.AllowAnyHeader();

            //////options.WithOrigins("eff.cpm", "xyz.com")
            //////.WithMethods("GET")
            //////.WithHeaders("Content-type", "Content-Length", "Authorization");

            ////// });

            app.UseMvc();
            //}
        }
    }
}
