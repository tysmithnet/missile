using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Conversion
{
    [Export(typeof(IConverterRepository))]
    public class ConverterRepository : IConverterRepository
    {
        protected internal List<RegisteredConverter> registeredConverters;

        // todo: import
        protected internal IConverterSelectionStrategy ConverterSelectionStrategy { get; set; } =
            new ConverterSelectionStrategy();

        [ImportMany(typeof(IConverter))]
        protected internal IConverter[] Converters { get; set; }

        protected internal IList<RegisteredConverter> RegisteredConverters =>
            registeredConverters ?? (registeredConverters = GetRegisteredConverters(Converters));

        public RegisteredConverter Get(Type source, Type dest)
        {
            var converters = ConverterSelectionStrategy.Select(RegisteredConverters, source, dest).ToList();
            if (!converters.Any())
                throw new IndexOutOfRangeException($"Unable to find a converter from {source} -> {dest}");
            return converters.First();
        }

        public void Add(RegisteredConverter registeredConverter)
        {
            if (registeredConverters == null)
                registeredConverters = new List<RegisteredConverter>();
            registeredConverters.Add(registeredConverter);
        }

        private List<RegisteredConverter> GetRegisteredConverters(IEnumerable<IConverter> converters)
        {
            var registeredConverters = new List<RegisteredConverter>();

            var mapping = converters.Select(c => new
            {
                Instance = c,
                Interfaces = c.GetType().GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConverter<,>)).ToList()
            }).Where(x => x.Interfaces.Any());

            foreach (var item in mapping)
            foreach (var iface in item.Interfaces)
                registeredConverters.Add(new RegisteredConverter
                {
                    ConverterInstance = item.Instance,
                    SourceType = iface.GenericTypeArguments[0],
                    DestType = iface.GenericTypeArguments[1],
                    ConvertMethodInfo = typeof(IConverter<,>)
                        .MakeGenericType(iface.GenericTypeArguments[0], iface.GenericTypeArguments[1])
                        .GetMethod("Convert")
                });

            return registeredConverters;
        }
    }
}