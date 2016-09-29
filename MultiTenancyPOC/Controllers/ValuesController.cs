using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MultiTenancyPOC.ActionFilters;
using MultiTenancyPOC.AuthenticationFilters;
using MultiTenancyPOC.Cache;
using MultiTenancyPOC.Cache.Enums;
using MultiTenancyPOC.Models;
using MultiTenancyPOC.Security;

namespace MultiTenancyPOC.Controllers
{
    [AuthenticationFilter]
    //[System.Web.Http.Authorize]
    //[TenantFilter]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [EnableCors("*", "*", "*")]
        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            IList<StudentModel> studentsList;
            HttpResponseMessage response;
            var currentPrincipal = Thread.CurrentPrincipal as UserExtended;

            if (currentPrincipal == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NonAuthoritativeInformation);
                return response;
            }

            var cache = CacheFactory.GetCache(CacheType.InMemoryObjectCache, CacheIdentifiers.StudentProfileCache);// Should be injected as static object

            if (!cache.Contains(StudentProfileCacheObjects.StudentProfileModel.ToString()))
            {
                studentsList = GetStudentProfile(currentPrincipal.TenantTypes);
                cache.Add(new CacheItem(StudentProfileCacheObjects.StudentProfileModel.ToString(), studentsList), new CacheItemPolicy());
            }
            else
            {
                studentsList = (IList<StudentModel>)cache.Get(StudentProfileCacheObjects.StudentProfileModel.ToString());
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

        private List<StudentModel> GetStudentProfile(TenantTypes tenantTypes)
        {
            if (tenantTypes == TenantTypes.Cal)
            {
                return new List<StudentModel>()
                {
                    new StudentModel() {City = "Oakville", LastName = "Borsuk", Name = "Alex"},
                    new StudentModel() {City = "Oakville", LastName = "Borsuk", Name = "Alex"},
                    new StudentModel() {City = "Oakville", LastName = "Borsuk", Name = "Alex"}
                };
            }

            return new List<StudentModel>()
            {
                new StudentModel() {City = "Toronto", LastName = "Borsuk", Name = "Alex"},
                new StudentModel() {City = "Toronto", LastName = "Borsuk", Name = "Alex"},
                new StudentModel() {City = "Toronto", LastName = "Borsuk", Name = "Alex"}
            };
        }
    }
}
