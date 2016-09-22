using System;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MultiTenancyPOC.ActionFilters;
using MultiTenancyPOC.Models;
using MultiTenancyPOC.Security;

namespace MultiTenancyPOC.Controllers
{
    [System.Web.Http.Authorize]
    [TenantFilter]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [System.Web.Http.AllowAnonymous]
        [EnableCors("*", "*", "*")]
        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            List<StudentModel> studentsList;
            HttpResponseMessage response;
            var currentPrincipal = Thread.CurrentPrincipal as UserExtended;

            if (currentPrincipal == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NonAuthoritativeInformation);
                return response;
            }

            if (currentPrincipal.TenantTypes == TenantTypes.Cal)
            {
                studentsList = new List<StudentModel>()
                {
                    new StudentModel() {City = "Oakville", LastName = "Borsuk", Name = "Alex"},
                    new StudentModel() {City = "Oakville", LastName = "Borsuk", Name = "Alex"},
                    new StudentModel() {City = "Oakville", LastName = "Borsuk", Name = "Alex"}
                };
            }
            else
            {
                studentsList = new List<StudentModel>()
            {
                new StudentModel() { City = "Toronto", LastName = "Borsuk", Name = "Alex"},
                new StudentModel() { City = "Toronto", LastName = "Borsuk", Name = "Alex"},
                new StudentModel() { City = "Toronto", LastName = "Borsuk", Name = "Alex"}
            };
            }

            response = Request.CreateResponse(HttpStatusCode.OK);
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(studentsList);
            response.Content = new StringContent(serializedResult, Encoding.UTF8, "application/json");
            return response;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
