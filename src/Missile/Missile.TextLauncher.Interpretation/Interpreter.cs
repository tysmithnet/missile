using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public interface IObservableInspector
    {
        bool CanHandle(Type type);
        Type GetObservableType(Type type);
    }

    [Export(typeof(IObservableInspector))]
    public class ToObservableInspector : IObservableInspector
    {
        public bool CanHandle(Type type)
        {
            return Regex.IsMatch(type?.FullName ?? "",
                @"^System\.Reactive\.Linq\.ObservableImpl\.ToObservable`1");
        }

        public Type GetObservableType(Type type)
        {
            return type.GenericTypeArguments[0];
        }
    }

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
                rootNode.DestinationNode = new DestinationNode(new DestinationToken("noop"));

            var destination =
                DestinationRepository.Get(rootNode.DestinationNode.Name);

            var providerResult = provider.Provide(rootNode.ProviderNode.ArgString);
            var toDestination = providerResult;
            if (!destination.SourceType.IsInstanceOfType(toDestination))
            {
                var inspector = ObservableInspectors.FirstOrDefault(i => i.CanHandle(toDestination.GetType()));
                if (inspector == null)
                    throw new ApplicationException($"Unable to find an inspector for {toDestination.GetType()}");
                var typeForConverter = inspector.GetObservableType(toDestination.GetType());
                var converter = ConverterRepository.Get(typeForConverter, destination.SourceType);
                toDestination = converter.Convert(toDestination);
            }

            var destinationTask = destination.Process(toDestination);
            return destinationTask;
        }
    }
}