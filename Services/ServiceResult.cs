using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Services;



public class ServiceResult<T>
{
    public T? Data { get; set; }

    public List<string>? ErrorMessage { get; set; }
    [JsonIgnore]
    public bool IsSuccess=>ErrorMessage == null || ErrorMessage.Count==0;
    [JsonIgnore]
    public bool IsFail=>!IsSuccess;
    [JsonIgnore]
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

public class ServiceResult
{
   

    public List<string>? ErrorMessage { get; set; }

    [JsonIgnore]
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    [JsonIgnore]
    public bool IsFail => !IsSuccess;
    [JsonIgnore]
    public HttpStatusCode Status { get; set; }


    //static factory methods
    public static ServiceResult Succecss( HttpStatusCode status = HttpStatusCode.OK)
    {
        return new ServiceResult()
        {
            
            Status = status
        };
    }
    public static ServiceResult Fail(List<string> errors, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {
            ErrorMessage = errors,
            Status = status
        };
    }
    public static ServiceResult Fail(string error, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new ServiceResult()
        {//  ErrorMessage = { error}
            ErrorMessage = new List<string>() { error }

            ,
            Status = status
        };
    }



}