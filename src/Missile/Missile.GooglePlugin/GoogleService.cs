using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Missile.Core;

namespace Missile.GooglePlugin
{
    public class GoogleService : IService
    {
        public string ServiceName { get; } = "google";
        public string Title { get; } = "Google";
        public string Description { get; } = "Search google for results";
        public Task SetupAsync()
        {
            return Task.FromResult(0);
        }

        public Task<object> DeleteAsync(string json)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<object> GetAsync(string json)
        {
            string apiKey = Environment.GetEnvironmentVariable("GOOGLE_SEARCH_API_KEY");
            string cseId = Environment.GetEnvironmentVariable("GOOGLE_SEARCH_CSE_KEY");

            HttpClient httpClient = new HttpClient();
            //string url = $"https://www.googleapis.com/customsearch/v1?key={apiKey}&cx={cseId}&q={query}";
            //string google = await httpClient.GetStringAsync(url);
            string google = File.ReadAllText(@"C:\Users\master\Documents\computing\projects\missile\src\Missile\Missile.GooglePlugin\sample.json");
            return google;
        }

        public Task<object> PatchAsync(string json)
        {
            throw new NotImplementedException();
        }

        public Task<object> PostAsync(string json)
        {
            throw new NotImplementedException();
        }

        public Task<object> PutAsync(string json)
        {
            throw new NotImplementedException();
        }
    }
}
