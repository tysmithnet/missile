using System;

namespace Missile.TextLauncher.Interpretation
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