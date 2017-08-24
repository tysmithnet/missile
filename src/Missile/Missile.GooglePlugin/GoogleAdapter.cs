using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Missile.GooglePlugin
{
    public class GoogleAdapter : IGoogleAdapter
    {
        public async Task<string> SearchAsync(string query)
        {
            string apiKey = Environment.GetEnvironmentVariable("GOOGLE_SEARCH_API_KEY");
            string cseId = Environment.GetEnvironmentVariable("GOOGLE_SEARCH_CSE_KEY");

            HttpClient httpClient = new HttpClient();
            //string url = $"https://www.googleapis.com/customsearch/v1?key={apiKey}&cx={cseId}&q={query}";
            //string google = await httpClient.GetStringAsync(url);
            string google = File.ReadAllText(@"C:\Users\master\Documents\computing\projects\missile\src\Missile\Missile.GooglePlugin\sample.json");
            return google;
        }
    }
}
