using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace ValidateToken.Filter
{
    public class AuthorizationFilter : IAuthorizationFilter
    { 
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            Microsoft.Extensions.Primitives.StringValues token;
            var controllerActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;
            
            if (filterContext.HttpContext.Request.Headers == null)
            {
                throw new Exception(@"Invalid request format");
            }
            else
            {
                filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out token);
            }

            ValidateUserToken validateUser = new ValidateUserToken();
            var IsAuthenticatedstatusCode = validateUser.ValidateToken(filterContext.HttpContext.Request.Headers["Authorization"]);
            if (IsAuthenticatedstatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(@"Invalid User");
            }
                
        }
    }
}

