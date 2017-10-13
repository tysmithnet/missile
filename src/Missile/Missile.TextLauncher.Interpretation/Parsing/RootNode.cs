using System.Collections.Generic;
using System.Linq;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    /// <summary>
    ///     Represents the root of the AST
    /// </summary>
    public class RootNode
    {
        /// <summary>
        ///     Gets the provider node.
        /// </summary>
        /// <value>
        ///     The provider node.
        /// </value>
        public ProviderNode ProviderNode { get; internal set; }

        /// <summary>
        ///     Gets the filter nodes.
        /// </summary>
        /// <value>
        ///     The filter nodes.
        /// </value>
        public List<FilterNode> FilterNodes { get; internal set; } = new List<FilterNode>();

        /// <summary>
        ///     Gets the destination node.
        /// </summary>
        /// <value>
        ///     The destination node.
        /// </value>
        public DestinationNode DestinationNode { get; internal set; }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is RootNode node))
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

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 74872637;
            if (ProviderNode != null)
                hashCode ^= ProviderNode.GetHashCode() % 790199;
            if (FilterNodes != null)
                foreach (var filter in FilterNodes)
                    hashCode ^= filter.GetHashCode() % 793489;
            if (DestinationNode != null)
                hashCode ^= DestinationNode.GetHashCode() % 796951;
            return hashCode;
        }
    }
}