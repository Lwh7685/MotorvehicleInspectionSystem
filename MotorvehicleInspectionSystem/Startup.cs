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
        /// log4net �ִ���
        /// </summary>
        //public static ILoggerRepository Repository { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            VehicleInspectionController.SyAj = Configuration.GetConnectionString("SyAj");
            var conSerAj = Configuration.GetConnectionString("ConnAj");//���������ļ�appsettings.json
            VehicleInspectionController.ConstrAj = conSerAj;//��ȡ������·�������ֶθ�ֵ��SqlHelper�������ľ�̬�ֶ�
            VehicleInspectionController.SyHj = Configuration.GetConnectionString("SyHj");
            var conSerHj = Configuration.GetConnectionString("ConnHj");//���������ļ�appsettings.json
            VehicleInspectionController.ConstrHj = conSerHj;//��ȡ������·�������ֶθ�ֵ��SqlHelper�������ľ�̬�ֶ�
            VehicleInspectionController.SyUb = Configuration.GetConnectionString("SyUB");
            var conSerUB = Configuration.GetConnectionString("ConnUB");//���������ļ�appsettings.json
            VehicleInspectionController.ConstrUB = conSerUB;//��ȡ������·�������ֶθ�ֵ��SqlHelper�������ľ�̬�ֶ�
            ////logע��ILoggerHelper
            //services.AddSingleton<ILoggerHelper, LoggerHelper>();

            ////log4net
            //Repository = LogManager.CreateRepository("MotorvehicleInspectionSystem");//��Ŀ����
            //XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));//ָ�������ļ���

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
                // ����ѭ������
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // ��ʹ���շ�
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // ����ʱ���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // ���ֶ�Ϊnullֵ�����ֶβ��᷵�ص�ǰ��
                // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            //����Swagger
            //ע��Swagger������������һ��Swagger �ĵ�
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "�ӿ��ĵ�",
                    Description = "RESTful API"

                });
                // Ϊ Swagger ����xml�ĵ�ע��·��
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
            //�����м����������Swagger
            app.UseSwagger();
            //�����м����������SwaggerUI��ָ��Swagger JSON�ս��
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web App V1");
                c.RoutePrefix = string.Empty;//���ø��ڵ����
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"InterfaceDocumentation")),
                RequestPath = new Microsoft.AspNetCore.Http.PathString("/InterfaceDocumentation")
            });
        }
    }
}
