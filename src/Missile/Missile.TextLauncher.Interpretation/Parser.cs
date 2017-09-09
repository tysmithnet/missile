using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(IParser))]
    public class Parser : IParser
    {
        public RootNode Parse(IEnumerable<Token> tokens)
        {
            if (tokens == null)
                throw new NullReferenceException(nameof(tokens));

            var list = tokens.ToList();

            if (!(list.FirstOrDefault() is ProviderToken))
                list.Insert(0, new ProviderToken("noop"));
            if (!(list.LastOrDefault() is DestinationToken))
                list.Add(new DestinationToken("noop"));

            if (list.OfType<ProviderToken>().Count() > 1)
                throw new ArgumentException("there can be only 1 provider token");

            if (list.OfType<DestinationToken>().Count() > 1)
                throw new ArgumentException("there can be only 1 destination token");

            return new RootNode
            {
                ProviderNode = new ProviderNode(list.First() as ProviderToken),
                FilterNodes = list.OfType<FilterToken>().Select(x => new FilterNode(x)).ToList(),
                DestinationNode = new DestinationNode(list.Last() as DestinationToken)
            };
        }
    }
}