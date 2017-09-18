using Missile.TextLauncher.Interpretation.Lexing;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    public class ProviderNode : Node
    {
        public ProviderNode(ProviderToken requestedProvider)
        {
            Name = requestedProvider.Name;
            Args = requestedProvider.Args;
        }
    }
}