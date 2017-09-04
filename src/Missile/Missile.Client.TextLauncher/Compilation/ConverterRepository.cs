using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Missile.Client.TextLauncher.Compilation
{
    public class ConverterRepository : IConverterRepository
    {
        public IReadOnlyCollection<IConverter> Converters { get; set; }

        private struct CacheKey
        {
            public Type SourceType { get; set; }
            public Type DestType { get; set; }
        }

        private Dictionary<CacheKey, IConverter> Cache { get; set; }

        public ConverterRepository(IEnumerable<IConverter> converters)
        {
            Converters = new ReadOnlyCollection<IConverter>(converters.ToList());
            converters.Select(x => new
            {
                Converter = x,
                Interfaces = x.GetType().GetInterfaces().Where(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConverter<,>))
            });
        }

        public IConverter Get(Type source, Type dest)
        {
            throw new NotImplementedException();
        }
    }
}