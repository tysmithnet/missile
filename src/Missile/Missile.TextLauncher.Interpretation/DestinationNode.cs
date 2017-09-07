namespace Missile.TextLauncher.Interpretation
{
    public class DestinationNode : Node
    {
        public DestinationNode(DestinationToken destinationToken)
        {
            Name = destinationToken.Identifier;
            ArgString = destinationToken.ArgString;
        }
    }
}