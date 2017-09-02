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
                                                                         
            if(list.Count(x => x is ProviderToken) > 1)
                throw new ArgumentException("cannot have more than 1 ProviderToken");

            if (list.Count(x => x is DestinationToken) > 1)
                throw new ArgumentException("cannot have more than 1 DestinationToken");

            if(!(list.Last() is DestinationToken))
                throw new ArgumentException("last token must be DestinationToken");

            if(list.OfType<OperatorToken>().Count(o => o.Identifier == ">") != 1)
                throw new ArgumentException("cannot have more than 1 output operator");

            for (int i = 0; i < list.Count; i++)
            {
                var token = list[i];
                if (i == 0)
                {
                    ProviderToken providerToken = token as ProviderToken;
                    if(providerToken == null)
                        throw new ArgumentException("first token must be ProviderToken");
                    root.ProviderNode = new ProviderNode(providerToken);
                    continue;
                }   
            }

            return root;
        }
    }
}