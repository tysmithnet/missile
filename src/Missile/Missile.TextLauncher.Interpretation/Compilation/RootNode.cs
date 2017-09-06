using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation.Compilation
{
    public class RootNode
    {
        public ProviderNode ProviderNode { get; set; }
        public List<FilterNode> FilterNodes { get; set; } = new List<FilterNode>();
        public DestinationNode DestinationNode { get; set; }
    }
}