using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Missile.Client.TextLauncher.Compilation
{
    public class ConverterEntry
    {
        public Type SourceType { get; set; }
        public Type DestType { get; set; }
        public IConverter Converter { get; set; }

        private Type interfaceType;
        public Type InterfaceType => interfaceType ?? (interfaceType = typeof(IConverter<,>).MakeGenericType(SourceType, DestType));

        private MethodInfo convertMethodInfo;
        public MethodInfo ConvertMethodInfo => convertMethodInfo ?? (convertMethodInfo = InterfaceType.GetMethod("Convert"));

        public override bool Equals(object obj)
        {
            var entry = obj as ConverterEntry;
            return entry != null &&
                   EqualityComparer<Type>.Default.Equals(SourceType, entry.SourceType) &&
                   EqualityComparer<Type>.Default.Equals(DestType, entry.DestType) &&
                   EqualityComparer<IConverter>.Default.Equals(Converter, entry.Converter);
        }

        public override int GetHashCode()
        {
            var hashCode = 1060857348;
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(SourceType);
            hashCode = hashCode * -1521134295 + EqualityComparer<Type>.Default.GetHashCode(DestType);
            hashCode = hashCode * -1521134295 + EqualityComparer<IConverter>.Default.GetHashCode(Converter);
            return hashCode;
        }
    }
             
    public class ConverterRepository : IConverterRepository
    {
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
            foreach (var type in precursor.Interfaces)
            {
                var curSource = type.GenericTypeArguments[0];
                var curDest = type.GenericTypeArguments[1];
                Lookup.Add(new ConverterEntry
                {
                    Converter = precursor.Converter,
                    SourceType = curSource,
                    DestType = curDest
                });
            }
        }

        public IReadOnlyCollection<IConverter> Converters { get; set; }

        protected internal List<ConverterEntry> Lookup { get; set; }

        public IEnumerable<ConverterEntry> Get(Type sourceType, Type destType)
        {
            return new DefaultConverterRetrievalStrategy().Get(Lookup, sourceType, destType);
        }
                                     
        protected internal interface IConverterRetrievalStrategy
        {
            IEnumerable<ConverterEntry> Get(IEnumerable<ConverterEntry> entries, Type requestedSource,
                Type requestedDest);
        }

        protected internal class DefaultConverterRetrievalStrategy : IConverterRetrievalStrategy
        {
            public IEnumerable<ConverterEntry> Get(IEnumerable<ConverterEntry> entries, Type requestedSource,
                Type requestedDest)
            {
                var candidates = entries.Where(e =>
                    e.SourceType.IsAssignableFrom(requestedSource) && requestedDest.IsAssignableFrom(e.DestType));

                var exactMatches = new List<ConverterEntry>();
                var partialMatches = new List<ConverterEntry>();
                var theRest = new List<ConverterEntry>();
                foreach (var candidate in candidates)
                {
                    var sameSource = requestedSource == candidate.SourceType;
                    var sameDest = requestedDest == candidate.DestType;
                    if (sameSource && sameDest)
                        exactMatches.Add(candidate);
                    else if (sameSource || sameDest)
                        partialMatches.Add(candidate);
                    else
                        theRest.Add(candidate);
                }

                return exactMatches.Concat(partialMatches).Concat(theRest);
            }
        }
    }
}