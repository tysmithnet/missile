using System.Threading.Tasks;

namespace Missile.Core
{
    public interface IService : IPlugin
    {
        string ServiceName { get; }
        Task<object> GetAsync(string query);
    }
}