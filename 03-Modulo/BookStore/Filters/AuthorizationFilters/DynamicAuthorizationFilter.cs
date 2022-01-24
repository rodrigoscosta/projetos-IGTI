using BookStore.Data.Contexto;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore.Filters.AuthorizationFilters
{
    public class DynamicAuthorizationFilter :  IAsyncAuthorizationFilter
    {
        private readonly LivrosDbContext livrosDbContext;

        public DynamicAuthorizationFilter(LivrosDbContext livrosDbContext)
        {
            this.livrosDbContext = livrosDbContext;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string token = string.Empty;
            //AUTHORIZATION
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorization);

            if (authorization == StringValues.Empty)
            {
                context.Result = new UnauthorizedObjectResult(new { status = "Fail", message = "Unauthorized." });
                return;
            }
            else
            {
                var authType = authorization.ToString().Split(' ')[0];
                token = authorization.ToString().Split(' ')[1];

                if (authType != "Basic" && string.IsNullOrEmpty(token))
                {
                    context.Result = new UnauthorizedObjectResult(new { status = "Fail", message = "Incorret Authentication Type." });
                }
            }

            //PAYLOAD
            var data = Convert.FromBase64String(token);
            var decoded = Encoding.UTF8.GetString(data);
            var userRequest = JsonSerializer.Deserialize<Usuario>(decoded);

            if (userRequest == null)
            {
                context.Result = new BadRequestObjectResult(new { status = "Fail", message = "Invalid Request." });
                return;
            }
            else
            {
                if (!userRequest.EstaValida())
                {
                    context.Result = new BadRequestObjectResult(new { status = "Fail", message = "Invalid Request." });
                    return;
                }
            }

            await Task.Delay(1);

            var usuario = livrosDbContext.Usuarios.SingleOrDefault(c => c.Name == userRequest.Name && c.Password == userRequest.Password);

            if (usuario == null)
            {
                context.Result = new BadRequestObjectResult(new { status = "Fail", message = "Invalid UserName or Password." });
                return;
            }

            if (usuario.Role != "admin")
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }
        } 
    }
}
