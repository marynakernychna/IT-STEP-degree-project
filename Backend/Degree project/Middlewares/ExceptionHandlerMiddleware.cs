using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionHandlerMiddleware(
            RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(
            HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (HttpException httpException)
            {
                await HandleGlobalExceptionAsync(
                    httpContext,
                    httpException.StatusCode,
                    httpException.Message);
            }
            catch (Exception exception)
            {
                await HandleGlobalExceptionAsync(
                    httpContext,
                    errorMessage: exception.Message);
            }
        }

        private static async Task HandleGlobalExceptionAsync(
            HttpContext httpContext,
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError,
            string errorMessage = "Unknown error has been occurred!")
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)httpStatusCode;

            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(errorMessage));
        }
    }
}
