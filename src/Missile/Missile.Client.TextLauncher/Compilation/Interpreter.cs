using System;
using System.Collections.Generic;
using System.Linq;
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
            //var providerOutputType = provider
            //    .GetType()
            //    .GetInterfaces()
            //    .First(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IProvider<>))
            //    .GenericTypeArguments[0];

            //var filterType = root.FilterNodes.First().GetType();
            //var filterInputType = filterType
            //    .GetInterfaces()
            //    .First(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IFilter<,>))
            //    .GenericTypeArguments[0];

            //var filterOutputType = filterType
            //    .GetInterfaces()
            //    .First(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IFilter<,>))
            //    .GenericTypeArguments[1];

            //var converter = ConverterRepository.Get(filterInputType, filterOutputType);

            var providerType = provider.GetType();
            var getMethodInfo = providerType.GetMethod("Get", new [] {typeof(string)});
            var result = getMethodInfo.Invoke(provider, new object[]{ root.ProviderNode.ArgString });
            var filterName = root.FilterNodes.First().Name;
            var filter = FilterRepository.Get(filterName);
            var filterType = filter.GetType();
            var filterMethodInfo = filterType.GetMethod("Filter");
            var filteredResult = filterMethodInfo.Invoke(filter, new[] {result});
            var destinationName = root.DestinationNode.Name;
            var destination = DestinationRepository.Get(destinationName);
            var destinationType = destination.GetType();
            var processMethodInfo = destinationType.GetMethod("Process");
            var processTask = processMethodInfo.Invoke(destination, new[] {filteredResult});
            Task task = processTask as Task;
            if(task == null)
                throw new ApplicationException();
            return task;
        }
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
