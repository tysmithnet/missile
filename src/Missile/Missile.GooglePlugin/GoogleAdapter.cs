using System.IO;  
using System.Threading.Tasks;
using Missile.Core;

namespace Missile.GooglePlugin
{
    public class GoogleAdapter : IGoogleAdapter
    {  
        internal IConfigurationService ConfigurationService { get; set; }
        internal IHttpService HttpService { get; set; }

        internal GoogleAdapter()
        {
            
        }

        public GoogleAdapter(IConfigurationService configurationService, IHttpService httpService)
        {
            ConfigurationService = configurationService;
            HttpService = httpService;
        }

        public async Task<string> SearchAsync(string query)
        {
            Options options = await ConfigurationService.GetConfigAsync<Options>("google");     
            string url = $"https://www.googleapis.com/customsearch/v1?key={options.ApiKey}&cx={options.CseKey}&q={query}";
            string json = await HttpService.GetStringAsync(url);
            //string google = File.ReadAllText(@"C:\Users\master\Documents\computing\projects\missile\src\Missile\Missile.GooglePlugin\sample.json");
            return json;
        }
    }
}
