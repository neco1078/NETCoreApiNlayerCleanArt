using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.ExceptionHandlers
{
    public class CriticalExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is CriticalException) {
                Console.WriteLine("hata ile ilgili smss gönderildi");
            
            }
            //response modeli burda belirlemeyeceğim globalexceptionhandlerda yapılacak
            return ValueTask.FromResult(false);
            //bu hatayı ben ele alıcam
           // return true;
            //businesslogic global exception handler bir sonraki handlera şutla
           // return false;
        }
    }
}
