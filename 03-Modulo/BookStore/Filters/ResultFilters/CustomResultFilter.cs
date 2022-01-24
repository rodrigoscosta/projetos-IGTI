using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BookStore.Filters.ResultFilters
{
    public class CustomResultFilter : Attribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result as ObjectResult;
            
            context.Result = new OkObjectResult(new { status = "Successo", statusCode = StatusCodes.Status200OK, data = result.Value });
        }
    }
}
