using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Missile.TextLauncher.Conversion;
using Missile.TextLauncher.Destination;
using Missile.TextLauncher.Filtration;
using Missile.TextLauncher.Interpretation.Lexing;
using Missile.TextLauncher.Interpretation.Parsing;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(IInterpreter))]
    public class Interpreter : IInterpreter
    {
        [Import]
        public IProviderRepository ProviderRepository { get; set; }

        [Import]
        public IFilterRepository FilterRepository { get; set; }

        [Import]
        public IDestinationRepository DestinationRepository { get; set; }

        [Import]
        public IConverterRepository ConverterRepository { get; set; }

        [ImportMany]
        public IObservableInspector[] ObservableInspectors { get; set; }

        public Task Interpret(RootNode rootNode)
        {
            var provider = ProviderRepository.Get(rootNode.ProviderNode.Name);

            if (rootNode.DestinationNode == null)
                rootNode.DestinationNode = new DestinationNode(new DestinationToken("noop", new string[0]));

            var destination =
                DestinationRepository.Get(rootNode.DestinationNode.Name);

            var providerResult = provider.Provide(rootNode.ProviderNode.Args);
            var destinationInput = providerResult;
            var inspector = ObservableInspectors.FirstOrDefault(i => i.CanHandle(destinationInput.GetType()));
            if (inspector == null)
                throw new ApplicationException($"Unable to find an inspector for {destinationInput.GetType()}");

            var typeForConverter = inspector.GetObservableType(destinationInput.GetType());

            if (!destination.SourceType.IsAssignableFrom(typeForConverter))
            {
                var converter = ConverterRepository.Get(typeForConverter, destination.SourceType);
                destinationInput = converter.Convert(destinationInput);
            }

            var destinationTask = destination.Process(destinationInput);
            return destinationTask;
        }
    }
}