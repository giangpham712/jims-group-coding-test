using System;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace JimsGroupCodingTest.LambdaWebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {

                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = contextFeature.Error;
                    string errorMessage;

                    if (exception.GetType() == typeof(InvalidOperationException) ||
                        exception.GetType() == typeof(ArgumentException))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorMessage = exception.Message;
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorMessage = "Error occurred processing request.";
                    }

                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Error = errorMessage
                    }, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }));
                });
            });
        }
    }
}
