using System.Web;
using System.Web.Mvc;
using MultiTenancyPOC.ActionFilters;

namespace MultiTenancyPOC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());           
        }
    }
}
