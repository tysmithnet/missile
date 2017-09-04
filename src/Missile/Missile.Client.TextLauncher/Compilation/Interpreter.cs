using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.Client.TextLauncher.Compilation
{
    public class Interpreter : IInterpreter
    {
        public IProviderRepository ProviderRepository { get; set; }
        public IConverterRepository ConverterRepository { get; set; }
        public IFilterRepository FilterRepository { get; set; }
        public IDestinationRepository DestinationRepository { get; set; }

        public Task Interpret(RootNode root)
        {
            string providerName = root.ProviderNode.Name;
            IProvider provider = ProviderRepository.Get(providerName);
            var providerType = provider.GetType();
            var getMethodInfo = providerType.GetMethod("Get", new [] {typeof(string)});
            var providerResult = getMethodInfo.Invoke(provider, new object[]{ root.ProviderNode.ArgString });
            var filterName = root.FilterNodes.First().Name;
            var filter = FilterRepository.Get(filterName);
            var filterType = filter.GetType();
            var filterMethodInfo = filterType.GetMethod("Filter");
            var converters = ConverterRepository.Get(providerResult.GetType().GenericTypeArguments[0],
                filterMethodInfo.ReturnType.GenericTypeArguments[0]);
            var converterEntry = converters.First();
            var convertedResult =
                converterEntry.ConvertMethodInfo.Invoke(converterEntry.Converter, new[] {providerResult});
            var filteredResult = filterMethodInfo.Invoke(filter, new[] {convertedResult});
            var destinationName = root.DestinationNode.Name;
            var destination = DestinationRepository.Get(destinationName);
            var destinationType = destination.GetType();
            var processMethodInfo = destinationType.GetMethod("Process");
            var destinationConverter = ConverterRepository.Get(typeof(StringBuilder), typeof(string)).First();
            var destinationConvertedResult =
                destinationConverter.ConvertMethodInfo.Invoke(destinationConverter.Converter, new[] {filteredResult});
            var processTask = processMethodInfo.Invoke(destination, new[] {destinationConvertedResult});
            Task task = processTask as Task;
            if(task == null)
                throw new ApplicationException();
            return task;
        }
    }


    public static class ExtensionMethods
    {
        
    }

    public interface IFilterRepository
    {
        IFilter Get(string name);
    }

    public class FilterRepository : IFilterRepository
    {

        public IReadOnlyCollection<IFilter> Filters { get; set; }
        private Dictionary<string, IFilter> Cache { get; set; }

        public FilterRepository(IReadOnlyCollection<IFilter> filters)
        {
            Filters = filters;
            Cache = new Dictionary<string, IFilter>();
        }

        public IFilter Get(string name)
        {
            if (Cache.ContainsKey(name))
                return Cache[name];

            throw new IndexOutOfRangeException($"no filter registered with name {name}");
        }
    }

    public interface IDestinationRepository
    {
        IDestination Get(string name);
    }

    public class DestinationRepository : IDestinationRepository
    {
        public IReadOnlyCollection<IDestination> Destinations { get; set; }

        private Dictionary<string, IDestination> Cache { get; set; }

        public DestinationRepository(IReadOnlyCollection<IDestination> destinations)
        {
            Destinations = destinations;
            Cache = new Dictionary<string, IDestination>();

        }

        public IDestination Get(string name)
        {
            throw new NotImplementedException();
        }
    }

    public interface IProviderRepository
    {
        IProvider Get(string name);
    }

    public class ProviderRepository : IProviderRepository
    {
        public IReadOnlyCollection<IProvider> Providers { get; set; }
        private Dictionary<string, IProvider> Cache { get; set; }

        public ProviderRepository(IReadOnlyCollection<IProvider> providers)
        {
            Providers = providers;
            Cache = new Dictionary<string, IProvider>();
            foreach (var provider in Providers)
            {
                var interfaces = provider.GetType().GetInterfaces()
                    .Where(t => typeof(IProvider).IsAssignableFrom(t) && t.GenericTypeArguments.Any()).ToList();

                if (!interfaces.Any())
                    continue;

                Cache.Add(provider.Name, provider);
            }
        }

        public IProvider Get(string name)
        {
            if (Cache.ContainsKey(name))
                return Cache[name];
            throw new IndexOutOfRangeException($"no provider registered with name {name}");
        }
    }
}
