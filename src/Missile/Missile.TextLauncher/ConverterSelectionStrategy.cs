using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher
{
    [Export(typeof(IConverterSelectionStrategy))]
    public class ConverterSelectionStrategy : IConverterSelectionStrategy
    {
        public IEnumerable<RegisteredConverter> Select(IEnumerable<RegisteredConverter> registeredConverters,
            Type source, Type dest)
        {
            return registeredConverters.Select(x => new
            {
                Converter = x,
                Score = GetConverterScore(x, source, dest)
            }).Where(x => x.Score != null).Select(x => x.Converter);
        }

        public ConverterRedinessScore GetConverterScore(RegisteredConverter registeredConverter, Type source, Type dest)
        {
            var converterSource = registeredConverter.SourceType;
            var converterDest = registeredConverter.DestType;

            int? sourceDistance = null;
            int? destDistance = null;

            var leftBreakdown = new TypeBreakDown(source);
            var converterSourceBreakdown = new TypeBreakDown(converterDest);

            if (source == converterSource)
                sourceDistance = 0;
            else if (leftBreakdown.Interfaces.Contains(converterSource))
                sourceDistance = 1;
            else if (leftBreakdown.BaseTypes.Contains(converterSource))
                sourceDistance = leftBreakdown.BaseTypes.IndexOf(converterSource);

            if (converterDest == dest)
                destDistance = 0;
            else if (converterSourceBreakdown.Interfaces.Contains(dest))
                destDistance = 1;
            else if (converterSourceBreakdown.BaseTypes.Contains(dest))
                destDistance = converterSourceBreakdown.BaseTypes.IndexOf(dest);

            if (sourceDistance == null || destDistance == null)
                return null;

            return new ConverterRedinessScore
            {
                SourceDistance = sourceDistance.Value,
                DestDistance = destDistance.Value
            };
        }
    }

    public class TypeBreakDown
    {
        public TypeBreakDown(Type type)
        {
            InstanceType = type;
            Interfaces.AddRange(type.GetInterfaces() ?? new Type[0]);
            BaseTypes = type.GetBaseTypes().ToList();
        }

        public Type InstanceType { get; set; }
        public List<Type> Interfaces { get; set; } = new List<Type>();
        public List<Type> BaseTypes { get; set; }
    }
}