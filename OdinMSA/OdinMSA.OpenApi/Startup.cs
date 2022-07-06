using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OdinMSA.OdinEF;
using OdinMSA.OdinLog;
using OdinMSA.OdinLog.Core;
using OdinMSA.OdinLog.Core.Models;
using OdinMSA.OpenApi.Services;

namespace OdinMSA.OpenApi
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
            services.AddControllers();
            services.AddSingleton<IPushRecordSignalRServices,PushRecordSignalRServices>();
            services.AddSingletonSqlSugar(Configuration,"OdinPush");
            services.AddOdinSingletonOdinLogs(opt=>
                opt.Config=new LogConfig {
                    LogSaveType=new EnumLogSaveType[]{EnumLogSaveType.All},
                    ConnectionString = "server=47.122.0.223;Database=OdinPush;Uid=root;Pwd=173djjDJJ;"});
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OdinMSA.OpenApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OdinMSA.OpenApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}