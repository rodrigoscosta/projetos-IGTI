
using BookStore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.Filters.ExceptionFilters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null) 
            {
                if (context.Exception is DbReadException)
                {
                    var message = context.Exception.Message;

                    context.Result = new ObjectResult(new { status = "Error", message = "Erro interno." });
                }
                else
                {
                    context.Result = new ObjectResult(new { status = "Error", message = "Erro interno." });
                }
            }
        } 
    }
}
