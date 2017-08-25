using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Missile.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Missile.Server
{
    public class ConfigurationService : IConfigurationService
    {
        internal static readonly string Config = File.ReadAllText("config.json");

        public Task<T> GetConfigAsync<T>(string provider)
        {
            JObject obj = JObject.Parse(Config);
            JProperty property = obj.Properties().Single(p => p.Name == provider);
            T converted = property.Value.ToObject<T>();
            return Task.FromResult(converted);
        }
    }
}
