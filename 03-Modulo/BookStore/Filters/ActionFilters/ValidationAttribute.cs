using BookStore.Models.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace BookStore.Filters.ActionFilters
{
    public class ValidationAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is IModel);

            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult(new { status = "Fail", message = "Objeto nulo" });
                return;
            }

            var model = param.Value as IModel;

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            if (model.EstaValida())
            {
                context.Result = new BadRequestObjectResult(new { status = "Fail", message = model.Error });
            }
        }
    }
}