using System;
using System.Collections.Generic;
using System.Linq;

namespace Missile.Client.TextLauncher
{
    public class Parser : IParser
    {
        public RootNode Parse(IEnumerable<Token> tokens)
        {
            var list = tokens.ToList();
            RootNode root = new RootNode();

            if (list == null || !list.Any())
                return root;

            ProviderToken providerToken = list.First() as ProviderToken ?? throw new ArgumentException("first token should be ProviderToken");
            root.ProviderNode = new ProviderNode(providerToken);

            if(list.Count(x => x is ProviderToken) > 1)
                throw new ArgumentException("cannot have more than 1 ProviderToken");

            if (list.Count(x => x is DestinationToken) > 1)
                throw new ArgumentException("cannot have more than 1 DestinationToken");

            if(!(list.Last() is DestinationToken))
                throw new ArgumentException("last token must be DestinationToken");

            return root;
        }
    }
}