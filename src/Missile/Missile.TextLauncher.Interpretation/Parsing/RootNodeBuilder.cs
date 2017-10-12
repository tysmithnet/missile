using System.Linq;
using Missile.TextLauncher.Interpretation.Lexing;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    /// <summary>
    /// Builder for RootNode
    /// </summary>
    internal class RootNodeBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootNodeBuilder"/> class.
        /// </summary>
        public RootNodeBuilder()
        {
            RootNode = new RootNode();
        }

        /// <summary>
        /// Gets or sets the root node.
        /// </summary>
        /// <value>
        /// The root node.
        /// </value>
        internal RootNode RootNode { get; set; }


        /// <summary>
        /// Sets the provider.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>this builder</returns>
        public RootNodeBuilder WithProvider(string providerName, string[] args)
        {
            RootNode.ProviderNode = new ProviderNode(new ProviderToken(providerName, args));
            return this;
        }

        /// <summary>
        /// Sets the provider.
        /// </summary>
        /// <param name="token">The token.</param>
        internal void WithProvider(ProviderToken token)
        {
            RootNode.ProviderNode = new ProviderNode(token);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>this builder</returns>
        public RootNode Build()
        {
            if (RootNode.ProviderNode == null && !RootNode.FilterNodes.Any() && RootNode.DestinationNode == null)
                return new RootNode();
            return RootNode;
        }

        /// <summary>
        /// Sets the destination.
        /// </summary>
        /// <param name="destintationToken">The destintation token.</param>
        /// <returns>this builder</returns>
        public RootNodeBuilder WithDestination(DestinationToken destintationToken)
        {
            RootNode.DestinationNode = new DestinationNode(destintationToken);
            return this;
        }

        /// <summary>
        /// Sets the destination.
        /// </summary>
        /// <param name="destinationName">Name of the destination.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>this builder</returns>
        public RootNodeBuilder WithDestination(string destinationName, string[] args)
        {
            RootNode.DestinationNode = new DestinationNode(new DestinationToken(destinationName, args));
            return this;
        }

        /// <summary>
        /// Sets the filter.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>this builder</returns>
        public RootNodeBuilder WithFilter(string filterName, string[] args)
        {
            RootNode.FilterNodes.Add(new FilterNode(new FilterToken(filterName, args)));
            return this;
        }
    }
}