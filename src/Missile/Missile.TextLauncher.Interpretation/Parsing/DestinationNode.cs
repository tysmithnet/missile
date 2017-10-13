using Missile.TextLauncher.Interpretation.Lexing;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a destination node in an abstract syntax tree
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Parsing.Node" />
    public class DestinationNode : Node
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DestinationNode" /> class.
        /// </summary>
        /// <param name="destinationToken">The destination token.</param>
        public DestinationNode(DestinationToken destinationToken)
        {
            Name = destinationToken.Name;
            Args = destinationToken.Args;
        }
    }
}