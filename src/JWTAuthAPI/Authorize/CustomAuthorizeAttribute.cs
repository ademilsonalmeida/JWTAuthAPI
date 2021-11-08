using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JWTAuthAPI.Authorize
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            allowedroles = roles;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            bool authorize = false;
            var authToken = actionContext.Request.Headers?.Authorization?.Parameter;

            if (authToken == null)
                return authorize;

            List<string> userRoles = TokenManager.GetPrincipal(authToken)?.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();

            if (userRoles != null && userRoles.Any())
                authorize = allowedroles.Any(x => userRoles.Any(y => y == x));

            return authorize;
        }
    }
}