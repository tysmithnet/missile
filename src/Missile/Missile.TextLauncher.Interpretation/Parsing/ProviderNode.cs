using Missile.TextLauncher.Interpretation.Lexing;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    /// <inheritdoc />
    /// <summary>
    ///     Abstract syntax tree node for providers
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Parsing.Node" />
    public class ProviderNode : Node
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ProviderNode" /> class.
        /// </summary>
        /// <param name="requestedProvider">The requested provider.</param>
        public ProviderNode(ProviderToken requestedProvider)
        {
            Name = requestedProvider.Name;
            Args = requestedProvider.Args;
        }
    }
}