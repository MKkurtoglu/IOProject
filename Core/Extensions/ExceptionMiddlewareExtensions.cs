using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app) //program.cs deki middleware ları biz bu interface ile yakalıyorduk.
                                                                                            //ve içerisine bunu da extend ettik.
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
