using System.Linq;

namespace Missile.TextLauncher.Interpretation
{
    internal class RootNodeBuilder
    {
        public RootNodeBuilder()
        {
            RootNode = new RootNode();
        }

        internal RootNode RootNode { get; set; }


        public RootNodeBuilder WithProvider(string providerName, string[] args)
        {
            RootNode.ProviderNode = new ProviderNode(new ProviderToken(providerName, args));
            return this;
        }

        internal void WithProvider(ProviderToken token)
        {
            RootNode.ProviderNode = new ProviderNode(token);
        }

        public RootNode Build()
        {
            if (RootNode.ProviderNode == null && !RootNode.FilterNodes.Any() && RootNode.DestinationNode == null)
                return new RootNode();
            return RootNode;
        }

        public RootNodeBuilder WithDestination(DestinationToken destintationToken)
        {
            RootNode.DestinationNode = new DestinationNode(destintationToken);
            return this;
        }

        public RootNodeBuilder WithDestination(string destinationName, string[] args)
        {
            RootNode.DestinationNode = new DestinationNode(new DestinationToken(destinationName, args));
            return this;
        }

        public RootNodeBuilder WithFilter(string filterName, string[] args)
        {
            RootNode.FilterNodes.Add(new FilterNode(new FilterToken(filterName, args)));
            return this;
        }
    }
}