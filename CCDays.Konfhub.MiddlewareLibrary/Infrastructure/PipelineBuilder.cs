using CCDays.Konfhub.MiddlewareLibrary.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CCDays.Konfhub.MiddlewareLibrary.Infrastructure
{
    public class PipelineBuilder : IPipelineBuilder
    {
        private readonly List<ServerlessMiddleware> _middlewarePipeline=new List<ServerlessMiddleware>();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PipelineBuilder(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public PipelineBuilder(List<ServerlessMiddleware> middlewarePipeline)
        {
            _middlewarePipeline = new List<ServerlessMiddleware>();
            
            foreach(var serverlessMiddleware in middlewarePipeline)
            {
                Use(serverlessMiddleware);
            }
        }

        public async Task<IActionResult> Execute()
        {
            var context = this._httpContextAccessor.HttpContext;

            if(_middlewarePipeline.Any())
            {
                await _middlewarePipeline.First().InvokeAsync(context);

                if (context.Response!=null)
                    return new Abstractions.HttpResponse(context);
            }

            throw new Exception("No middleware configured");
        }

        public IPipelineBuilder Use(ServerlessMiddleware middleware)
        {
            if (_middlewarePipeline is null) throw new Exception("Middleware pipeline is not registerd");

            if(_middlewarePipeline?.Count()>0)
            {
                _middlewarePipeline.Last().Next = middleware;
            }
            
            _middlewarePipeline.Add(middleware);

            return this;
        }
    }
}
