using System.Collections.Generic;
using System.Linq;

namespace Missile.Client.TextLauncher.Compilation
{
    public class RootNode
    {
        public ProviderNode ProviderNode { get; set; }
        public List<FilterNode> FilterNodes { get; set; }
        public DestinationNode DestinationNode { get; set; }

        public override bool Equals(object obj)
        {
            var node = obj as RootNode;
            if (node == null)
                return false;
            bool providerEqual = ProviderNode.Equals(node.ProviderNode);
            bool filtersEqual = (FilterNodes == null && node.FilterNodes == null) ||
                                   FilterNodes.Intersect(node.FilterNodes).Count() ==
                                   FilterNodes.Union(node.FilterNodes).Count();
            bool destinationEqual = DestinationNode.Equals(node.DestinationNode);
            return providerEqual && filtersEqual && destinationEqual;
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
