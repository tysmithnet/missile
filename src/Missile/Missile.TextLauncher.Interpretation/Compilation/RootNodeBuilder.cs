using System;
using System.Linq;

namespace Missile.TextLauncher.Interpretation.Compilation
{
    internal class RootNodeBuilder
    {
        public RootNodeBuilder()
        {
            RootNode = new RootNode();
        }

        internal RootNode RootNode { get; set; }


        public RootNodeBuilder WithProvider(string providerName)
        {
            RootNode.ProviderNode = new ProviderNode(new ProviderToken(providerName));
            return this;
        }

        internal void WithProvider(ProviderToken token)
        {
            RootNode.ProviderNode = new ProviderNode(token);
        }

        public RootNode Build()
        {
            if(RootNode.ProviderNode == null && !RootNode.FilterNodes.Any() && RootNode.DestinationNode == null)
                return new RootNode();
            return RootNode;
        }

        public RootNodeBuilder WithDestination(DestinationToken destintationToken)
        {
            RootNode.DestinationNode = new DestinationNode(destintationToken);
            return this;
        }

        public RootNodeBuilder WithDestination(string destinationName)
        {
            RootNode.DestinationNode = new DestinationNode(new DestinationToken(destinationName));
            return this;
        }

        public RootNodeBuilder WithFilter(string filterName)
        {
            RootNode.FilterNodes.Add(new FilterNode(new FilterToken(filterName)));
            return this;
        }
    }
}