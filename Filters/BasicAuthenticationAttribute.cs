using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiVagas.Filters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public const string TOKEN = "admin";

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authorizationHeader = actionContext.Request.Headers.Authorization;
            if (authorizationHeader ==null || authorizationHeader.Scheme != "Bearer" || authorizationHeader.Parameter != TOKEN)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}