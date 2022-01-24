using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BookStore.Filters.AuthorizationFilters
{
    public class HttpOnlyAttribute : Attribute, IAuthorizationFilter
    {
        #region Methods
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.IsHttps)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        } 
        
        #endregion
    }
}
