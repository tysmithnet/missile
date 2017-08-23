using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                           
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Services.Select(x => x.ServiceName);
        }

        [HttpGet("/help/{service}")]
        public string Get(string service)
        {
            return "help is on the way";
        }
                           
        [HttpGet("{service}/{query}")]
        public async Task<object> Get(string service, string query)
        {
            return await Services.Single(x => x.ServiceName == service).GetAsync(query);
        }
                           
        [HttpPost("{service}")]
        public object Post(string service, [FromBody]object value)
        {
            return new
            {
                Service = service
            };
        }
                           
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]object value)
        {
        }
                              
        [HttpDelete("{id}")]
        public void Delete(object value)
        {
        }
    }
}
