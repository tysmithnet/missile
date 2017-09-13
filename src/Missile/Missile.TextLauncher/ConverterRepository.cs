using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;

namespace Missile.TextLauncher
{


    public interface IConverterSelectionStrategy
    {
        IEnumerable<RegisteredConverter> Select(IEnumerable<RegisteredConverter> registeredConverters, Type source, Type dest);
    }

    [Export(typeof(IConverterSelectionStrategy))]
    public class ConverterSelectionStrategy : IConverterSelectionStrategy
    {
        public class ConverterRedinessScore
        {
            public int SourceDistance { get; set; }
            public int DestDistance { get; set; }
        }

        public IEnumerable<RegisteredConverter> Select(IEnumerable<RegisteredConverter> registeredConverters, Type source, Type dest)
        {
            return registeredConverters.Select(x => new
            {
                Converter = x,
                Score = ScoreConverter(x, source, dest)
            }).Where(x => x.Score != null).Select(x => x.Converter);
        }

        public ConverterRedinessScore ScoreConverter(RegisteredConverter registeredConverter, Type source, Type dest)
        {
            var sourceTypes = source.GetBaseTypes().ToList();
            int sourceDistance = 0;
            int destDistance = 0;
            if (sourceTypes.Contains(registeredConverter.SourceType))
                sourceDistance = sourceTypes.IndexOf(registeredConverter.SourceType);
            else
                return null;

            if (registeredConverter.DestTypes.Contains(dest))
                destDistance = registeredConverter.DestTypes.IndexOf(dest);
            else
                return null;
            
            return new ConverterRedinessScore
            {
                SourceDistance = sourceDistance,
                DestDistance = destDistance
            };
        }
    }

    [Export(typeof(IConverterRepository))]
    public class ConverterRepository : IConverterRepository
    {
        internal List<RegisteredConverter> registeredConverters;

        
        public IConverterSelectionStrategy ConverterSelectionStrategy { get; set; } = new ConverterSelectionStrategy();

        [ImportMany(typeof(IConverter))]
        public IConverter[] Converters { get; set; }

        protected internal IList<RegisteredConverter> RegisteredConverters =>
            registeredConverters ?? (registeredConverters = GetRegisteredConverters(Converters));

        private List<RegisteredConverter> GetRegisteredConverters(IEnumerable<IConverter> converters)
        {
            List<RegisteredConverter> registeredConverters = new List<RegisteredConverter>();

            var mapping = converters.Select(c => new
            {
                Instance = c,
                Interfaces = c.GetType().GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConverter<,>)).ToList()
            }).Where(x => x.Interfaces.Any());

            foreach (var item in mapping)
            foreach(var iface in item.Interfaces)
                    registeredConverters.Add(new RegisteredConverter
                    {
                        ConverterInstance = item.Instance,
                        SourceType = iface.GenericTypeArguments[0],
                        DestTypes = iface.GenericTypeArguments[1].GetBaseTypes().ToList(),
                        ConvertMethodInfo = typeof(IConverter<,>).MakeGenericType(iface.GenericTypeArguments[0], iface.GenericTypeArguments[1]).GetMethod("Convert")
                    });

            return registeredConverters;
        }
        
        public RegisteredConverter Get(Type source, Type dest)
        {
            var converters = ConverterSelectionStrategy.Select(RegisteredConverters, source, dest).ToList();
            if(!converters.Any())
                throw new IndexOutOfRangeException($"Unable to find a converter from {source} -> {dest}");
            return converters.First();
        }
    }
}