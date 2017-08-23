using System;
using System.Threading.Tasks;
using Missile.Core;

namespace Missile.EverythingPlugin
{
    public class EverythingService : IService
    {
        public string ServiceName { get; } = "everything";
        public string Title { get; } = "Everything provider";
        public string Description { get; } = "Searches for files using the Everything application";

        public Task<object> GetAsync(string query)
        {
            return Task.FromResult<object>(0);
        }
    }
}
                                                                                                           