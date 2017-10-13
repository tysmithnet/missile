using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Conversion
{
    /// <inheritdoc />
    /// <summary>
    ///     Repository for converters that transform observables of one type into
    ///     observables of another type
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Conversion.IConverterRepository" />
    [Export(typeof(IConverterRepository))]
    public class ConverterRepository : IConverterRepository
    {
        /// <summary>
        ///     Source of truth for registered converters
        /// </summary>
        protected internal List<RegisteredConverter> registeredConverters;

        /// <summary>
        ///     Strategy implementation that determines which converter to use given a request
        /// </summary>
        /// <value>
        ///     The converter selection strategy.
        /// </value>
        [Import]
        protected internal IConverterSelectionStrategy ConverterSelectionStrategy { get; set; }

        /// <summary>
        ///     Converter implementation instances
        /// </summary>
        /// <value>
        ///     The converters.
        /// </value>
        [ImportMany]
        protected internal IConverter[] Converters { get; set; }

        /// <summary>
        ///     RegisteredConverter getter
        /// </summary>
        /// <value>
        ///     The registered converters.
        /// </value>
        protected internal IList<RegisteredConverter> RegisteredConverters =>
            registeredConverters ?? (registeredConverters = GetRegisteredConverters(Converters));

        /// <summary>
        ///     Gets a converter for the conversion from source -&gt; dest
        /// </summary>
        /// <param name="source">Source type of the conversion</param>
        /// <param name="dest">Destination type of the conversion</param>
        /// <returns>
        ///     A suitable RegisteredConverter for the conversion of source -&gt; dest
        /// </returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <inheritdoc />
        public RegisteredConverter Get(Type source, Type dest)
        {
            var converters = ConverterSelectionStrategy.Select(RegisteredConverters, source, dest).ToList();
            if (!converters.Any())
                throw new KeyNotFoundException($"Unable to find a converter from {source} -> {dest}");
            return converters.First();
        }

        /// <summary>
        ///     Registers a new converter with this repository
        /// </summary>
        /// <param name="registeredConverter">Converter to register</param>
        /// <exception cref="ArgumentNullException">registeredConverter</exception>
        /// <inheritdoc />
        public void Register(RegisteredConverter registeredConverter)
        {
            if (registeredConverter == null)
                throw new ArgumentNullException(
                    $"{nameof(registeredConverter)} cannot be null because the repository will not hold null values");
            if (registeredConverters == null)
                registeredConverters = new List<RegisteredConverter>();
            registeredConverters.Add(registeredConverter);
        }

        /// <summary>
        ///     Transforms converter instances to RegisteredConverters
        /// </summary>
        /// <param name="converters">Converter instances to transform</param>
        /// <returns>
        ///     Transformed converter instances
        /// </returns>
        private List<RegisteredConverter> GetRegisteredConverters(IEnumerable<IConverter> converters)
        {
            var mapping = converters.Select(c => new
            {
                Instance = c,
                Interfaces = c.GetType().GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConverter<,>)).ToList()
            }).Where(x => x.Interfaces.Any());

            return (from item in mapping
                from iface in item.Interfaces
                select new RegisteredConverter
                {
                    ConverterInstance = item.Instance,
                    SourceType = iface.GenericTypeArguments[0],
                    DestType = iface.GenericTypeArguments[1],
                    ConvertMethodInfo = typeof(IConverter<,>)
                        .MakeGenericType(iface.GenericTypeArguments[0], iface.GenericTypeArguments[1])
                        .GetMethod("Convert")
                }).ToList();
        }
    }
}