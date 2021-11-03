using Microsoft.AspNetCore.Http;
using Models.ViewModels;
using Newtonsoft.Json;
using RESTApi.Services.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate requestDelegate;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private static Task HandleException(HttpContext context, Exception ex)
        {
            var errorMessageObject = new Error { Message = ex.Message, Code = "GE" };
            var statusCode = (int)HttpStatusCode.InternalServerError;
            switch (ex)
            {
                case ItemNotFoundException:
                    errorMessageObject.Code = "404";
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case InvalidAuthenticationException:
                    errorMessageObject.Code = "403";
                    statusCode = (int)HttpStatusCode.Forbidden;
                    break;

                case InvalidCredentialsException:
                    errorMessageObject.Code = "401";
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    break;
            }

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
