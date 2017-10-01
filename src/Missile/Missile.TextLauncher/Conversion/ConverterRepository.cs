using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;           

namespace Missile.TextLauncher.Conversion
{
    /// <summary>
    /// Repository for converters that transform observables of one type into
    /// observables of another type
    /// </summary>
    [Export(typeof(IConverterRepository))]
    public class ConverterRepository : IConverterRepository
    {
        /// <summary>
        /// Source of truth for registered converters
        /// </summary>
        protected internal List<RegisteredConverter> registeredConverters;
        
        /// <summary>
        /// Strategy implementation that determines which converter to use given a request
        /// </summary>
        [Import]
        protected internal IConverterSelectionStrategy ConverterSelectionStrategy { get; set; }

        /// <summary>
        /// Converter implementation instances
        /// </summary>
        [ImportMany]
        protected internal IConverter[] Converters { get; set; }

        /// <summary>
        /// RegisteredConverter getter
        /// </summary>
        protected internal IList<RegisteredConverter> RegisteredConverters =>
            registeredConverters ?? (registeredConverters = GetRegisteredConverters(Converters));

        /// <inheritdoc />
        public RegisteredConverter Get(Type source, Type dest)
        {
            var converters = ConverterSelectionStrategy.Select(RegisteredConverters, source, dest).ToList();
            if (!converters.Any())
                throw new KeyNotFoundException($"Unable to find a converter from {source} -> {dest}");
            return converters.First();
        }

        /// <inheritdoc />
        public void Register(RegisteredConverter registeredConverter)
        {
            if(registeredConverter == null)
                throw new ArgumentNullException($"{nameof(registeredConverter)} cannot be null because the repository will not hold null values");
            if (registeredConverters == null)
                registeredConverters = new List<RegisteredConverter>();
            registeredConverters.Add(registeredConverter);
        }

        /// <summary>
        /// Transforms converter instances to RegisteredConverters
        /// </summary>
        /// <param name="converters">Converter instances to transform</param>
        /// <returns>Transformed converter instances</returns>
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