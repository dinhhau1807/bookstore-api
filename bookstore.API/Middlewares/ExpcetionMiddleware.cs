using bookstore.Shared.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
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
        private readonly ILogger _logger;

        public ExpcetionMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (!(ex is AppException)) _logger.Error(ex.Message);
                await HandleExpceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExpceptionAsync(HttpContext httpContext, Exception exception)
        {
            var isAppException = exception is AppException;
            var appException = isAppException ? (AppException)exception : null;

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = isAppException ? appException.StatusCode : StatusCodes.Status500InternalServerError;

            object body = new
            {
                Success = false,
                ErrorMessage = isAppException ? appException.Message : "Something went wrong!",
                ErrorCode = httpContext.Response.StatusCode
            };

            if (_env.IsDevelopment())
            {
                body = new
                {
                    Success = false,
                    ErrorMessage = exception.Message,
                    ErrorStack = exception.StackTrace,
                    ErrorCode = httpContext.Response.StatusCode,
                    IsOperation = isAppException
                };
            }

            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(body, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
        }
    }
}
