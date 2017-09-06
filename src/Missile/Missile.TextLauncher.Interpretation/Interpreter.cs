using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class Interpreter : IInterpreter
    {       
        public IProviderRepository ProviderRepository { get; set; }

        public Task Interpret(RootNode rootNode)
        {
            var provider = ProviderRepository.RegisteredProviders.Single(x => x.Name == rootNode.ProviderNode.Name);
            NoopDestination noopDestination = new NoopDestination();
            return noopDestination.ProcessAsync((IObservable<object>) provider.Provide());
        }
    }
}
