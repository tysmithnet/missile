namespace Missile.TextLauncher.Interpretation
{
    internal class RootNodeBuilder
    {
        public RootNodeBuilder()
        {
            RootNode = new RootNode();
        }

        internal RootNode RootNode { get; set; }


        public RootNodeBuilder WithProvider(string noop)
        {
            RootNode.ProviderNode = new ProviderNode(new ProviderToken(noop));
            return this;
        }

        public RootNode Build()
        {
            return RootNode;
        }

        public RootNodeBuilder WithDestination(string destination)
        {
            RootNode.DestinationNode = new DestinationNode(new DestinationToken(destination));
            return this;
        }

        public RootNodeBuilder WithFilter(string log)
        {
            RootNode.FilterNodes.Add(new FilterNode(new FilterToken(log)));
            return this;
        }
    }
}