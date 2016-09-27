using System.Security.Principal;

namespace MultiTenancyPOC.AuthenticationFilters.Contracts
{
    public class IdentityExtended : IIdentity
    {
        public IdentityExtended(string name, string authenticationType, bool isAuthenticated)
        {
            Name = name;
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
        }

        public string Name { get; }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }
    }
}