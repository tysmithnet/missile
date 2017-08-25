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

        internal IGoogleAdapter GoogleAdapter { get; set; }                

        internal GoogleService()
        {
            
        }

        public GoogleService(IGoogleAdapter googleAdapter)
        {
            GoogleAdapter = googleAdapter;              
        }

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

        public async Task<object> GetAsync(string query)
        {
            return await GoogleAdapter.SearchAsync(query);
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
