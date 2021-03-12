using CCDays.Konfhub.ServerlessMiddleware.NET5.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace CCDays.Konfhub.ServerlessMiddleware.NET5
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {        
        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure dependencies 
        /// </summary>
        /// <param name="services"></param>
        internal void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<FormatResponseMiddleware>();
            services.AddSingleton<BypassMiddleware>();
            services.AddSingleton<PassThroughMiddleware>();
            services.AddSingleton<ExceptionHandlerMiddleware>();
            services.AddLogging();            
        }
        /// <summary>
        /// Configure Functions worker
        /// </summary>
        /// <param name="builder"></param>
        internal void ConfigureWorker(IFunctionsWorkerApplicationBuilder builder)
        {

           #region Middleware section to demonstrate format response scenario

            builder.Use(next =>
            {
                return context =>
                {
                    var middleware = context.InstanceServices.GetRequiredService<FormatResponseMiddleware>();

                    return middleware.Invoke(context, next);
                };
            });

            builder.UseFunctionExecutionMiddleware();
            
            #endregion

            #region Middleware section to demonstrate bypass scenario

            builder.Use(next =>
            {
                return context =>
                {
                    var bypassMiddleware = context.InstanceServices.GetRequiredService<BypassMiddleware>();

                    return bypassMiddleware.Invoke(context, next);
                };
            });

            builder.UseFunctionExecutionMiddleware();
            
            #endregion

            #region Middleware section to demonstrate pass through scenario
            
            builder.Use(next =>
            {
                return context =>
                {
                    var passThroughMiddleware = context.InstanceServices.GetRequiredService<PassThroughMiddleware>();

                    return passThroughMiddleware.Invoke(context, next);
                };
            });

            builder.UseFunctionExecutionMiddleware();

            #endregion

            #region Middleware section to demonstrate Exception handling scenario

            builder.Use(next =>
            {
                return context =>
                {
                    var exceptionHandlerMiddleware = context.InstanceServices.GetRequiredService<ExceptionHandlerMiddleware>();

                    return exceptionHandlerMiddleware.Invoke(context, next);
                };
            });

            builder.UseFunctionExecutionMiddleware();

            #endregion

        }
    }
}
