using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;
using MotorvehicleInspectionSystem.Controllers;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using NLog.Extensions.Logging;

namespace MotorvehicleInspectionSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        /// <summary>
        /// log4net 仓储库
        /// </summary>
        //public static ILoggerRepository Repository { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            VehicleInspectionController.SyAj = Configuration.GetConnectionString("SyAj");
            var conSerAj = Configuration.GetConnectionString("ConnAj");//连接配置文件appsettings.json
            VehicleInspectionController.ConstrAj = conSerAj;//获取到连接路径，将字段赋值给SqlHelper控制器的静态字段
            VehicleInspectionController.SyHj = Configuration.GetConnectionString("SyHj");
            var conSerHj = Configuration.GetConnectionString("ConnHj");//连接配置文件appsettings.json
            VehicleInspectionController.ConstrHj = conSerHj;//获取到连接路径，将字段赋值给SqlHelper控制器的静态字段
            VehicleInspectionController.SyUb = Configuration.GetConnectionString("SyUB");
            var conSerUB = Configuration.GetConnectionString("ConnUB");//连接配置文件appsettings.json
            VehicleInspectionController.ConstrUB = conSerUB;//获取到连接路径，将字段赋值给SqlHelper控制器的静态字段
            ////log注入ILoggerHelper
            //services.AddSingleton<ILoggerHelper, LoggerHelper>();

            ////log4net
            //Repository = LogManager.CreateRepository("MotorvehicleInspectionSystem");//项目名称
            //XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));//指定配置文件，

            services.AddLogging(logBuilder => {
                logBuilder.AddNLog();
            }
            );
            //services.Configure<KestrelServerOptions>(options =>
            //{
            //    // Set the limit to 256 MB
            //    options.Limits.MaxRequestBodySize = 268435456;
            //});
            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.MaxRequestBodySize = 1073741822;

            //});
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // 忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // 不使用驼峰
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // 设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // 如字段为null值，该字段不会返回到前端
                // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            //配置Swagger
            //注册Swagger生成器，定义一个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "接口文档",
                    Description = "RESTful API"

                });
                // 为 Swagger 设置xml文档注释路径
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //启用中间件服务生成Swagger
            app.UseSwagger();
            //启用中间件服务生成SwaggerUI，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web App V1");
                c.RoutePrefix = string.Empty;//设置根节点访问
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"InterfaceDocumentation")),
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/InterfaceDocumentation")
            });
        }
    }
}
