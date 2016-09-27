using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using MultiTenancyPOC.Security;

namespace MultiTenancyPOC.ActionFilters
{
    public class TenantFilter: System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            IEnumerable<string> headers;
            var tenantExists = actionContext.Request.Headers.TryGetValues("tenant_type", out headers);

            if (tenantExists)
            {
                var tenant = headers.FirstOrDefault();

                var principal = new UserExtended { TenantTypes = tenant == "CAL" ? TenantTypes.Cal : TenantTypes.Nslcsp };
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}