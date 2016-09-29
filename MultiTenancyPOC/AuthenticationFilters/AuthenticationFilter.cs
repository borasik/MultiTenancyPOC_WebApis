using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using MultiTenancyPOC.AuthenticationFilters.Contracts;
using MultiTenancyPOC.Security;

namespace MultiTenancyPOC.AuthenticationFilters
{ 
    public class AuthenticationFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        public Dictionary<string, string> Dictionary = new Dictionary<string, string>()
        {
            {"alexToken", "AlexUser"}
        };

        public UserExtended UserExtended { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {            
            IEnumerable<string> headers;
            var tenantExists = actionContext.Request.Headers.TryGetValues("tenant_type", out headers);
            if (tenantExists)
            {
                var tenant = headers.FirstOrDefault();
                UserExtended = new UserExtended { TenantTypes = tenant == "CAL" ? TenantTypes.Cal : TenantTypes.Nslcsp };                
            }


            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                var authToken = actionContext.Request.Headers.Authorization.Scheme;
                var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                if (FindUser(decodedToken))
                {
                    SetUpPrincipal();
                }
            }

            Thread.CurrentPrincipal = UserExtended;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = UserExtended;
            }
        }

        private void SetUpPrincipal()
        {
            if (UserExtended != null)
            {
                UserExtended.Identity = new IdentityExtended("", "", true);
                return;
            }

            UserExtended = new UserExtended {Identity = new IdentityExtended("", "", true)};
        }

        private bool FindUser(string token)
        {
            string user;            
            return Dictionary.TryGetValue(token, out user);
        }
    }        
}