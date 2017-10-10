using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public interface IRequiresSetup
    {
        Task SetupAsync(CancellationToken cancellationToken);
    }
}