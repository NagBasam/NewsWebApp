using FluentMigrator.Runner;
using HRA.News.API.Client;
using HRA.News.Business;
using HRA.News.Core;
using HRA.News.Core.ApplicationDbContext;
using HRA.News.Core.Migrations;
using HRA.News.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Reflection;

namespace HRA.News.Web
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
            services.AddRazorPages();
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/News/Index", "");
            });
            services.AddSingleton<INewsClient, NewsClient>();
            services.AddSingleton<IArticlesBusinessLayer, ArticlesBusinessLayer>();
            services.AddSingleton<IArticlesRepository, ArticlesRepository>();
            services.AddSingleton<IDapperContext, DapperContext>();
            services.AddSingleton<IDataBase, Database>();
            services.AddLogging(c => c.AddFluentMigratorConsole())
                    .AddFluentMigratorCore()
                    .ConfigureRunner(config =>
                                   config.AddSqlServer()
                    .WithGlobalConnectionString(Configuration.GetConnectionString("SqlConnection"))
                 .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
             .AddLogging(config => config.AddFluentMigratorConsole());

            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.AddSupportedCultures(new string[] { "en", "ar" })
                       .AddSupportedUICultures(new string[] { "en", "ar" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                if (context.Request.Query.Count > 0 &&
                context.Request.Query["Language"].ToString() != "")
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture =
                     System.Threading.Thread.CurrentThread.CurrentUICulture
                    = new CultureInfo(context.Request.Query["Language"].ToString());
                    //save cuurrent culture in cookie
                    context.Response.Cookies.Append(
                        CookieRequestCultureProvider.DefaultCookieName,
                        CookieRequestCultureProvider.MakeCookieValue
                        (new RequestCulture(context.Request.Query["Language"].ToString()))
                        , new CookieOptions() { Expires = DateTime.Now.AddHours(1) }
                        );

                }
                else
                {
                    context.Response.Cookies.Append(
                        CookieRequestCultureProvider.DefaultCookieName,
                        CookieRequestCultureProvider.MakeCookieValue
                        (new RequestCulture("en"))
                        , new CookieOptions() { Expires = DateTime.Now.AddHours(1) }
                        );
                }

                await next.Invoke();
            });

            app.UseRequestLocalization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
