namespace Missile.TextLauncher.Interpretation
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