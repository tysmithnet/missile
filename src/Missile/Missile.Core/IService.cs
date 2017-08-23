using System.Threading.Tasks;

namespace Missile.Core
{
    public interface IService : IPlugin
    {
        string ServiceName { get; }
        Task<object> DeleteAsync(string json);
        Task<object> GetAsync();
        Task<object> GetAsync(string query);
        Task<object> PatchAsync(string json);
        Task<object> PostAsync(string json);
        Task<object> PutAsync(string json); 
    }
}