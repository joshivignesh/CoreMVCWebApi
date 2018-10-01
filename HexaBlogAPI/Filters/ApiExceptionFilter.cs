using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HexaBlogAPI.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //throw new NotImplementedException();

            HttpStatusCode status = HttpStatusCode.InternalServerError;

            context.Result = new JsonResult(new { StatusCode = status, Message = context.Exception.Message });
        }
    }
}
