using Missile.TextLauncher.Interpretation.Lexing;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    public class DestinationNode : Node
    {
        public DestinationNode(DestinationToken destinationToken)
        {
            Name = destinationToken.Name;
            Args = destinationToken.Args;
        }
    }
}