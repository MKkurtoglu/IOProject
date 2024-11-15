
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Base.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        //RequestDelegate next parametresi, bu middleware'den sonra gelen bir sonraki middleware bileşenini temsil eder ve _next değişkenine atanır.
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
              
                await _next(httpContext);
                
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }
        //InvokeAsync metodu, her HTTP isteği geldiğinde çağrılır. Bu metodun görevi, HTTP isteklerini yönetmek ve hataları ele almaktır.

        //HttpContext parametresi, istek ve yanıtla ilgili bilgileri içeren nesnedir.
        //try bloğu içinde, _next(httpContext) ifadesi çalıştırılarak bir sonraki middleware'in çağrılması sağlanır. Bu, isteklerin zincir halinde ilerlemesini sağlar.
        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // ISE olacak diye bir şey yok. her türlü codu verebiliriz.
            //httpContext.Response.StatusCode = (int)HttpStatusCode.Continue; gibi..

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors;
            if (e.GetType() == typeof(ValidationException))
            {
             
                message = e.Message;  
                errors= ((ValidationException) e).Errors;
                httpContext.Response.StatusCode = 400;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    Message=e.Message,
                    Error=errors,
                    StatusCode=400
                }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
