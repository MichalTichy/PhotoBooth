using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBooth.DEMO.API.Filters
{
    //ignores requests containing queries in URI
    public class QueryFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Request.Query.Count != 0)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
