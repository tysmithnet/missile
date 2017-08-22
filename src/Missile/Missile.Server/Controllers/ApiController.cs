using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Missile.Core;

namespace Missile.Server.Controllers
{
    [Route("api/")]
    public class ApiController : Controller
    {
        public List<IPlugin> Plugins { get; set; }

        public ApiController(IEnumerable<IPlugin> plugins)
        {
            Plugins = plugins.ToList();
        }

        // GET api/
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{provider}/{query}")]
        public object Get(string provider, string query)
        {
            return Plugins.Select(x => x.GetType());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
