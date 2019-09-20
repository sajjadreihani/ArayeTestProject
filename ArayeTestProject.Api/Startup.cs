using ArayeTestProject.Api.Presistences.Context;
using ArayeTestProject.Api.Presistences.IRepositories;
using ArayeTestProject.Api.Presistences.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ArayeTestProject.Api {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor> ();
            services.AddDbContext<AppDbContext> (options => options.UseSqlServer (Configuration["ConnectionStrings:Default"]));
            services.AddMediatR (typeof (Startup));
            services.Configure<FormOptions> (x => {
                x.MultipartBodyLengthLimit = int.MaxValue;
            });

            services.AddTransient<IMainRepository, MainRepository> ();
            //In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles (configuration => {
                configuration.RootPath = "ClientApp/dist/browser";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.UseStaticFiles ();
            app.UseSpaStaticFiles ();

            app.UseHttpsRedirection ();
            app.UseMvc ();
            app.UseSpa (spa => {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment ()) {
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                    spa.UseAngularCliServer (npmScript: "start");
                }
            });

        }
    }
}