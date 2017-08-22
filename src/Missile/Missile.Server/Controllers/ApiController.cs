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
        public List<IProvider> Providers { get; set; }

        public ApiController(IEnumerable<IProvider> providers)
        {
            Providers = providers.ToList();
        }

        // GET api/
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Providers.Select(x => x.ProivderName);
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
        public object Post(string provider, string command, [FromBody]string value)
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
