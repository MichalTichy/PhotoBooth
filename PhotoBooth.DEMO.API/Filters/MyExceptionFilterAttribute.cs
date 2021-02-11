using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoBooth.DEMO.API.Filters
{
    public class MyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new Microsoft.AspNetCore.Mvc.JsonResult("Not a valid API call.");
            context.ExceptionHandled = true;
        }
    }
}
