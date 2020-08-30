using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace bookstore.API.Middlewares
{
    public class ExpcetionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExpcetionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // log error
                await HandleExpceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExpceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            object body = new
            {
                Successful = false,
                ErrorDesciption = "Something went wrong!",
                ErrorCode = StatusCodes.Status500InternalServerError
            };

            if (_env.IsDevelopment())
            {
                body = new
                {
                    Successful = false,
                    ErrorMessage = exception.Message,
                    ErrorStack = exception.StackTrace,
                    ErrorCode = StatusCodes.Status500InternalServerError
                };
            }

            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(body, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
        }
    }
}
