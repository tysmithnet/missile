using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation
{
    public class RootNode
    {
        public ProviderNode ProviderNode { get; set; }
        public List<FilterNode> FilterNodes { get; set; }
        public DestinationNode DestinationNode { get; set; }
    }
}