using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using CCDays.Konfhub.MiddlewareLibrary.Abstractions;
using CCDays.Konfhub.MiddlewareLibrary.Infrastructure;
using CCDays.Konfhub.ServerlessMiddleware.Pipeline;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

[assembly: FunctionsStartup(typeof(CCDays.Konfhub.ServerlessMiddleware.Startup))]
namespace CCDays.Konfhub.ServerlessMiddleware
{
    public class Startup : FunctionsStartup
    {
        /// <summary>
        /// Configured swagger UI for azure functions
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly(), opts =>
             {
                 opts.SpecVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
                 opts.AddCodeParameter = true;
                 opts.XmlPath = "CCDays.Konfhub.ServerlessMiddleware.xml";
                 opts.PrependOperationWithRoutePrefix = true;
                 opts.Documents = new[]
                 {
                     new SwaggerDocument
                     {
                         Name="v1",
                         Title="CCDays.Konfhub.ServerlessMiddleware",
                         Description="Swagger documentations for middleware implementation in Azure functions",
                         Version="v1"
                     }
                 };
                 opts.Title = "Serverless Middleware Service";
                 opts.ConfigureSwaggerGen = (x =>
                 {
                     x.EnableAnnotations();
                     x.CustomOperationIds(apiDesc =>
                     {
                         return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : new Guid().ToString();
                     });
                 });
             });

            ConfigureServices(builder.Services);
            builder.Services.AddLogging();
        }
        /// <summary>
        /// Register Dependencies
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddTransient<IPipelineFactory, PipelineFactory>();
        }
    }
}
