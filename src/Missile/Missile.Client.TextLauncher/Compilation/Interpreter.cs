using System;
using System.Collections.Generic;
using System.Linq;

namespace Missile.Client.TextLauncher.Compilation
{
    public class Interpreter : IInterpreter
    {
        public IProviderRepository ProviderRepository { get; set; }
        public IConverterRepository ConverterRepository { get; set; }
        public IDestinationRepository DestinationRepository { get; set; }
        
        public void Interpret(RootNode root)
        {

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
