﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Missile.GooglePlugin
{
    public class GoogleProvider
    {
        public async Task<string> SearchAsync(string query)
        {
            string apiKey = Environment.GetEnvironmentVariable("GOOGLE_SEARCH_API_KEY");
            string cseId = Environment.GetEnvironmentVariable("GOOGLE_SEARCH_CSE_KEY");

            HttpClient httpClient = new HttpClient();
            string url = $"https://www.googleapis.com/customsearch/v1?key={apiKey}&cx={cseId}&q={query}";
            string google = await httpClient.GetStringAsync(url);
            return google;
        }
    }
}
