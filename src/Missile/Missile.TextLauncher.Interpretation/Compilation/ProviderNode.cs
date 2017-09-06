namespace Missile.TextLauncher.Interpretation.Compilation
{
    public class ProviderNode : Node
    {
        public ProviderNode(ProviderToken requestedProvider)
        {
            Name = requestedProvider.Identifier;
            ArgString = requestedProvider.ArgString;
        }
    }
}