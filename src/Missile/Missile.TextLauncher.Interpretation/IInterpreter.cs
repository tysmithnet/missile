using System.Threading;
using System.Threading.Tasks;
using Missile.TextLauncher.Interpretation.Parsing;

namespace Missile.TextLauncher.Interpretation
{
    /// <summary>
    /// Represents an object that is capable of interpreting the instructions described in an abstract syntax tree
    /// created by an IParser instance
    /// </summary>
    public interface IInterpreter
    {
        /// <summary>
        /// Interprets the AST asynchronously
        /// </summary>
        /// <param name="rootNode">The root node.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task that when complete will signal the completion of the interpretation</returns>
        Task InterpretAsync(RootNode rootNode, CancellationToken cancellationToken);
    }
}