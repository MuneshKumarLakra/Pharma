using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using PharmaCoreApi.Helper;
using PharmaCoreApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaCoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        private static readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var _couchDbUser = Configuration["CouchDB:User"];
            var dbUserByteArray = Encoding.ASCII.GetBytes(_couchDbUser);
            

            services.AddControllers();
            services.AddHttpClient();
            services.AddSingleton<ICouchRepository, CouchRepository>();
            services.AddSingleton<IFileRepository, FileRepository>();
            services.AddHttpClient("Couchdb",c =>
            {
                c.BaseAddress = new Uri(Configuration["CouchDB:URL"]);
                c.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(dbUserByteArray));
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080");
                    });
            });

            services.AddResponseCompression();
            services.AddResponseCaching();

            // services.AddApplicationInsightsTelemetry(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseHttpsRedirection();
            app.ConfigureExceptionHandler(logger);
            app.UseRouting();
            app.UseResponseCompression();
            app.UseResponseCaching();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute("default", "{controller}/{action}");
            //});
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:8080");
                builder.AllowAnyHeader();
                builder.WithExposedHeaders("Token-Expired");
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                builder.Build();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
