using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Missile.Client.TextLauncher.Compilation
{
    public interface ICommand
    {
        Task ExecuteAsync();
    }

    public class PipelineCommand : ICommand
    {


        public Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class Interpreter : IInterpreter
    {
        public IProviderRepository ProviderRepository { get; set; }
        public IConverterRepository ConverterRepository { get; set; }
        public IFilterRepository FilterRepository { get; set; }
        public IDestinationRepository DestinationRepository { get; set; }

        public void Interpret(RootNode root)
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
