﻿using System;
using ArayeTestProject.Api.Presistences.Context;
using ArayeTestProject.Api.Presistences.IRepositories;
using ArayeTestProject.Api.Presistences.Repositories;
using AutoMapper;
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
            services.AddAutoMapper (AppDomain.CurrentDomain.GetAssemblies ());
            services.AddTransient<IMainRepository, MainRepository> ();
            services.AddSpaStaticFiles (configuration => {
                configuration.RootPath = "AdminPanel/dist/browser";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }
            app.UseStaticFiles ();
            app.UseSpaStaticFiles ();

            app.UseHttpsRedirection ();
            app.UseMvc ();
            app.UseSpa (spa => {
                spa.Options.SourcePath = "AdminPanel";
                if (env.IsDevelopment ()) {
                    spa.UseAngularCliServer (npmScript: "start");
                }
            });

        }
    }
}