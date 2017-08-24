using System.Threading.Tasks;

namespace Missile.GooglePlugin
{
    public interface IGoogleAdapter
    {
        Task<string> SearchAsync(string query);
    }
}