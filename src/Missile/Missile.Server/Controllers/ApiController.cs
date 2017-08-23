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
        public List<IService> Services { get; set; }

        public ApiController(IEnumerable<IService> services)
        {
            Services = services.ToList();
        }

        // GET api/
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Services.Select(x => x.ServiceName);
        }

        // GET api/values/5
        [HttpGet("{provider}/{query}")]
        public object Get(string provider, string query)
        {
            return new
            {
                Provider = provider,
                Query = query
            };
        }

        // POST api/values
        [HttpPost("{provider}/{command}")]
        public object Post(string provider, string command, [FromBody]Dictionary<string, string> value)
        {
            return new
            {
                Provider = provider,
                Command = command,
                Value = value
            };
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
