namespace Missile.Client.TextLauncher.Compilation
{
    public class ProviderNode : Node
    {                           
        public ProviderNode(ProviderToken providerToken)
        {
            Name = providerToken.Identifier;
            ArgString = providerToken.ArgString;
        }
    }
}
