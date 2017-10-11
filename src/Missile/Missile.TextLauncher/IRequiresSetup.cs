using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    /// <summary>
    ///     Represents an object that requires setup
    /// </summary>
    public interface IRequiresSetup
    {
        /// <summary>
        ///     Performs setup operations
        /// </summary>
        /// <param name="cancellationToken">The cancellation token to use when requesting that this operation be cancelled</param>
        /// <returns>A Task that indicates the completion of this setup</returns>
        Task SetupAsync(CancellationToken cancellationToken);
    }
}