using System;
using System.Collections.Generic;
using System.Linq;

namespace Missile.TextLauncher.Interpretation.Compilation
{
    public class Parser : IParser
    {
        public RootNode Parse(IEnumerable<Token> tokens)
        {   
            RootNodeBuilder builder = new RootNodeBuilder();

            

            return builder.Build();
        }
    }
}