using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class ProviderNode
    {
        public string RequestedProvider { get; set; }
        public string ArgString { get; set; }

        public ProviderNode(string requestedProvider, string argString)
        {
            RequestedProvider = requestedProvider ?? throw new ArgumentNullException(nameof(requestedProvider));
            ArgString = argString ?? throw new ArgumentNullException(nameof(argString));
        }
    }

    public class RootNode
    {
        public ProviderNode ProviderNode { get; set; }
    }

    public interface IInterpreter
    {
        Task Interpret(RootNode rootNode);
    }

    public class Interpreter : IInterpreter
    {
        public Task Interpret(RootNode rootNode)
        {
            return Task.FromException(new Exception("crashed"));
        }
    }
}
