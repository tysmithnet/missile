using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(IParser))]
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


            var root = new RootNode();
            root.ProviderNode = new ProviderNode(list.First() as ProviderToken);

            return root;
        }
    }
}