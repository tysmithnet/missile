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
    }
}