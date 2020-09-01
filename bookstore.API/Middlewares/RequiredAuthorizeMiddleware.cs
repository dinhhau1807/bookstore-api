using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.API.Middlewares
{
    public class RequiredAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public RequiredAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _next(httpContext);

            if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized ||
                httpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                httpContext.Response.ContentType = "application/json";

                object body = new
                {
                    Success = false,
                    ErrorMessage = httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized ?
                                                    "You're not logged in! Please log in to get access!" :
                                                    "You're not allowed to perform this action!",
                    ErrorCode = httpContext.Response.StatusCode
                };


                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(body, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
            }
        }
    }
}
