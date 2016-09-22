using System;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MultiTenancyPOC.Models;

namespace MultiTenancyPOC.Controllers
{
    [System.Web.Http.Authorize]
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
            var studentsList = new List<StudentModel>()
            {
                new StudentModel() { City = "Oakville", LastName = "Borsuk", Name = "Alex"},
                new StudentModel() { City = "Oakville", LastName = "Borsuk", Name = "Alex"},
                new StudentModel() { City = "Oakville", LastName = "Borsuk", Name = "Alex"}
            };

            var response = Request.CreateResponse(HttpStatusCode.OK);
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
