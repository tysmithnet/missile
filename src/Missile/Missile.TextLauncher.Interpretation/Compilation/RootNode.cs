using System.Collections.Generic;
using System.Linq;

namespace Missile.TextLauncher.Interpretation.Compilation
{
    public class RootNode
    {
        public ProviderNode ProviderNode { get; internal set; }
        public List<FilterNode> FilterNodes { get; internal set; } = new List<FilterNode>();
        public DestinationNode DestinationNode { get; internal set; }

        public override bool Equals(object obj)
        {
            var node = obj as RootNode;
            return node != null &&
                   EqualityComparer<ProviderNode>.Default.Equals(ProviderNode, node.ProviderNode) &&
                   EqualityComparer<List<FilterNode>>.Default.Equals(FilterNodes, node.FilterNodes) &&
                   EqualityComparer<DestinationNode>.Default.Equals(DestinationNode, node.DestinationNode);
        }

        public override int GetHashCode()
        {
            var hashCode = -74872637;
            hashCode = hashCode * -1521134295 + EqualityComparer<ProviderNode>.Default.GetHashCode(ProviderNode);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<FilterNode>>.Default.GetHashCode(FilterNodes);
            hashCode = hashCode * -1521134295 + EqualityComparer<DestinationNode>.Default.GetHashCode(DestinationNode);
            return hashCode;
        }
    }
}