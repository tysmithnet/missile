using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    /// <summary>
    /// Represents an object capable of executing a text based instruction for this launcher
    /// </summary>
    public interface IInterpretationFacade
    {
        /// <summary>
        /// Executes the provided textual command
        /// </summary>
        /// <param name="input">The input command to be executed</param>
        /// <param name="cancellationToken">Token to interrogate for cancellation requests</param>
        /// <returns>A task representing when the execution is complete</returns>
        Task ExecuteAsync(string input, CancellationToken cancellationToken);
    }
}