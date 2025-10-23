using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services;



public class ServiceResult<T>
{
    public T? Data { get; set; }

    public List<string>? ErrorMessage { get; set; }

    public bool IsSuccess=>ErrorMessage == null || ErrorMessage.Count==0;

    public bool IsFail=>!IsSuccess;

    public HttpStatusCode Status {get;set;}


    //static factory methods
    public static ServiceResult<T> Succecss(T data,HttpStatusCode status=HttpStatusCode.OK)
    {
        return new ServiceResult<T>()
        {
            Data = data,
            Status=status
        };
    }
    public static ServiceResult<T> Fail(List<string> errors, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {
            ErrorMessage = errors,
            Status=status
        };
    }
    public static ServiceResult<T> Fail(string error, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T>()
        {//  ErrorMessage = { error}
            ErrorMessage = new List<string>() { error }
          
            ,
            Status = status
        };
    }



}
