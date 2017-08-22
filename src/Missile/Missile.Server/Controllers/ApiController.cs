using System;
using System.Collections.Generic;   
using Microsoft.AspNetCore.Mvc;
using Missile.Core;

namespace Missile.Server.Controllers
{
    [Route("api/")]
    public class ApiController : Controller
    {
        public IPlugin Plugin { get; set; }

        public ApiController(IPlugin plugin)
        {
            Plugin = plugin;
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
            return Plugin.GetType();
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
