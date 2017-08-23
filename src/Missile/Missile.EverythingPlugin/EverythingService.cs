using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Missile.Core;

namespace Missile.EverythingPlugin
{
    public class EverythingService : IService
    {                                            

        public string ServiceName { get; } = "everything";
        public string Title { get; } = "Everything provider";
        public string Description { get; } = "Searches for files using the Everything application";

        public async Task SetupAsync()
        {
            
        }

        public Task<object> DeleteAsync(string json)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync()
        {
            throw new NotImplementedException();
        }
              
        public Task<object> GetAsync(string query)
        {
            var results = EverythingFacade.Search(query);
            return Task.FromResult<object>(results);
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
                                                                                                           