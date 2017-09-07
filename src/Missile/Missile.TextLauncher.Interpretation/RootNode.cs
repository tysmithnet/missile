using System.Collections.Generic;
using System.Linq;

namespace Missile.TextLauncher.Interpretation
{
    public class RootNode
    {
        public ProviderNode ProviderNode { get; internal set; }
        public List<FilterNode> FilterNodes { get; internal set; } = new List<FilterNode>();
        public DestinationNode DestinationNode { get; internal set; }

        public override bool Equals(object obj)
        {
            var node = obj as RootNode;
            if (node == null)
                return false;
            var providersBothNull = ProviderNode == null && node.ProviderNode == null;
            var providersAreSame = ProviderNode?.Equals(node.ProviderNode) ?? false;
            var filterNodesEqual = FilterNodes.Intersect(node.FilterNodes).Count() ==
                                   FilterNodes.Union(node.FilterNodes).Count();
            var destinationsBothNull = DestinationNode == null && node.DestinationNode == null;
            var destiantionsAreEqual = DestinationNode?.Equals(node.DestinationNode) ?? false;
            return (providersBothNull || providersAreSame) && filterNodesEqual &&
                   (destinationsBothNull || destiantionsAreEqual);
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