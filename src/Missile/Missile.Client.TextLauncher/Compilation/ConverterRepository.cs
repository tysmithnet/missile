using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Missile.Client.TextLauncher.Compilation
{
    public class ConverterRepository : IConverterRepository
    {     
        public IReadOnlyCollection<IConverter> Converters { get; set; }

        protected internal List<ConverterEntry> Lookup { get; set; }

        protected internal struct ConverterEntry
        {
            public Type SourceType { get; set; }
            public Type DestType { get; set; }
            public IConverter Converter { get; set; }
        }                  

        public ConverterRepository(IEnumerable<IConverter> converters)
        {
            Converters = new ReadOnlyCollection<IConverter>(converters.ToList());
            var precursors = Converters.Select(c => new
            {
                Converter = c,
                Interfaces = c.GetType().GetInterfaces().Where(type =>
                    type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IConverter<,>))
            }).ToList();

            Lookup = new List<ConverterEntry>();
            foreach (var precursor in precursors)
            {
                foreach (var type in precursor.Interfaces)
                {
                    Type curSource = type.GenericTypeArguments[0];
                    Type curDest = type.GenericTypeArguments[1];
                    Lookup.Add(new ConverterEntry
                    {
                        Converter = precursor.Converter,
                        SourceType = curSource,
                        DestType = curDest
                    });
                }
            }
        }

        public IEnumerable<IConverter> Get(Type sourceType, Type destType)
        {
            return Lookup.Where(x => x.SourceType.IsAssignableFrom(sourceType) && destType.IsAssignableFrom(x.DestType)).Select(x => x.Converter);
        }
    }
}