using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Missile.Core;

namespace Missile.Server
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient httpClient;

        public HttpService()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GetStringAsync(string url)
        {
            return await httpClient.GetStringAsync(url);
        }
    }
}
