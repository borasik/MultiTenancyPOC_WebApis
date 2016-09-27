using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MultiTenancyPOC.Security
{
    public class UserExtended: IPrincipal
    {
        public bool IsInRole(string role)
        {
            return true;
        }

        public IIdentity Identity { get; set; }

        public TenantTypes TenantTypes { get; set; }
    }

    public enum TenantTypes
    {
        Nslcsp = 1,
        Cal = 2
    }

    public class Identity : IIdentity
    {
        public string Name { get; }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
    }
}