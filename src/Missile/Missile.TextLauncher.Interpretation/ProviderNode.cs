using System;

namespace Missile.TextLauncher.Interpretation
{
    public class ProviderNode
    {
        public string RequestedProvider { get; set; }
        public string ArgString { get; set; }

        public ProviderNode(string requestedProvider, string argString)
        {
            RequestedProvider = requestedProvider ?? throw new ArgumentNullException(nameof(requestedProvider));
            ArgString = argString ?? throw new ArgumentNullException(nameof(argString));
        }

        public ProviderNode(ProviderToken requestedProvider)
        {
            throw new NotImplementedException();
        }
    }
}