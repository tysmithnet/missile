using System;
using System.Collections.Generic;
using System.Linq;

namespace Missile.TextLauncher.Interpretation.Compilation
{
    public class Parser : IParser
    {
        public RootNode Parse(IEnumerable<Token> tokens)
        {
            List<Token> list = tokens?.ToList() ?? new List<Token>();
            RootNodeBuilder builder = new RootNodeBuilder();
            builder.WithProvider(list.First() as ProviderToken);
            return builder.Build();
        }
    }
}