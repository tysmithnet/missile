using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Missile.Core;

namespace Missile.GooglePlugin
{
    public class GoogleAdapter : IGoogleAdapter
    {  
        internal IConfigurationService ConfigurationService { get; set; }

        public GoogleAdapter(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        public async Task<string> SearchAsync(string query)
        {
            Options options = await ConfigurationService.GetConfigAsync<Options>("google");
            HttpClient httpClient = new HttpClient();
            string url = $"https://www.googleapis.com/customsearch/v1?key={options.ApiKey}&cx={options.CseKey}&q={query}";
            //string google = await httpClient.GetStringAsync(url);
            string google = File.ReadAllText(@"C:\Users\master\Documents\computing\projects\missile\src\Missile\Missile.GooglePlugin\sample.json");
            return google;
        }
    }
}
