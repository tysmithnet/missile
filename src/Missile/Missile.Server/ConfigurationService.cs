using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Missile.Core;

namespace Missile.Server
{
    public class ConfigurationService : IConfigurationService
    {
        public Task<string> GetPropJsonAsync(string prop)
        {
            return Task.FromResult("{\"a\": 1 }");
        }
    }
}
