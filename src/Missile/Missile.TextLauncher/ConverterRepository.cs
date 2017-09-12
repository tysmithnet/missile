using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;

namespace Missile.TextLauncher
{
    public class RegisteredConverter
    {
        public IConverter ConverterInstance { get; set; }
        public Type Sourcetype { get; set; }
        public Type DestType { get; set; }
        public MethodInfo ConvertMethodInfo { get; set; }

        public object Convert(object source)
        {
            return ConvertMethodInfo.Invoke(ConverterInstance, new object[] {source});
        }
    }

    [Export(typeof(IConverterRepository))]
    public class ConverterRepository : IConverterRepository
    {
        internal List<RegisteredConverter> registeredConverters;

        [ImportMany(typeof(IConverter))]
        public IEnumerable<IConverter> Converters { get; set; }

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
                        Sourcetype = iface.GenericTypeArguments[0],
                        DestType = iface.GenericTypeArguments[1],
                        ConvertMethodInfo = typeof(IConverter<,>).MakeGenericType(iface.GenericTypeArguments[0], iface.GenericTypeArguments[1]).GetMethod("Convert")
                    });

            return registeredConverters;
        }

        public RegisteredConverter Get(Type source, Type dest)
        {
            RegisteredConverter converter;

            Type destItr = dest;
            Type sourceItr = source;

            while (destItr != null)
            {
                while (sourceItr != null)
                {
                    converter =
                        RegisteredConverters.FirstOrDefault(c => c.Sourcetype == sourceItr && c.DestType == destItr);
                    if (converter != null)
                        return converter;
                    sourceItr = sourceItr.BaseType;
                }
                destItr = destItr.BaseType;
            }

            converter = RegisteredConverters.FirstOrDefault(c =>
                c.Sourcetype.IsAssignableFrom(source) && c.DestType.IsAssignableFrom(dest));

            if (converter != null)
                return converter;
            throw new ArgumentOutOfRangeException($"Unable to find a converter from {source} -> {dest}");    
        }
    }
}