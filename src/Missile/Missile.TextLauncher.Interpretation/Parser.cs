using System;
using System.Collections.Generic;
using System.Linq;

namespace Missile.TextLauncher.Interpretation
{
    public class Parser : IParser
    {
        public RootNode Parse(IEnumerable<Token> tokens)
        {
            if (tokens == null)
                throw new ArgumentNullException(nameof(tokens));

            var list = tokens.ToList();

            if (list.Count(x => x is ProviderToken) != 1)
                throw new ArgumentException("tokens must have exactly 1 ProviderToken");

            if (!(list.First() is ProviderToken))
                throw new ArgumentException("tokens must start with a ProviderToken");

            if (list.Count(x => x is DestinationToken) > 1)
                throw new ArgumentException("cannot have more than 1 DestinationToken");

            if (!(list.Last() is DestinationToken))
                throw new ArgumentException("last token must be DestinationToken");

            if (list.OfType<OperatorToken>().Count(o => o.Identifier == ">") != 1)
                throw new ArgumentException("cannot have more than 1 output operator");

            var root = new RootNode();
            root.ProviderNode = new ProviderNode(list.First() as ProviderToken);

            root.FilterNodes = list.OfType<FilterToken>().Select(x => new FilterNode(x)).ToList();
            root.DestinationNode = new DestinationNode(list.Last() as DestinationToken);

            return root;
        }
    }
}