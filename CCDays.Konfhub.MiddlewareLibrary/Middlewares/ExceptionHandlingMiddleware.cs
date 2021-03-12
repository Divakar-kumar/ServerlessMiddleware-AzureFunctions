using CCDays.Konfhub.MiddlewareLibrary.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCDays.Konfhub.MiddlewareLibrary.Middlewares
{
    public class ExceptionHandlingMiddleware : ServerlessMiddleware
    {
        public override async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await HttpResponseWritingExtensions.WriteAsync(context.Response, "\n Request stage - Exception Handling middleware!");

                await this.Next.InvokeAsync(context);

                await HttpResponseWritingExtensions.WriteAsync(context.Response, "\n Response stage - Exception Handling middleware!");
            }
            catch (Exception ex)
            {
                await Task.CompletedTask;
            }
        }
    }
}
