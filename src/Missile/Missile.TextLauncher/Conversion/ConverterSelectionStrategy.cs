using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher.Conversion
{
    /// <summary>
    /// Default IConverterSelectionStrategy pattern.
    /// It scores in this way:
    ///     1. same type -> 
    ///     2. requested type implements converter type -> 1
    ///     3. requested type is related to base types -> distance
    /// </summary>
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

        /// <summary>
        /// Score a RegisteredCovnerter against a conversion
        /// </summary>
        /// <param name="registeredConverter">Converter to test</param>
        /// <param name="source">Source type of the conversion</param>
        /// <param name="dest">Destination type of the conversion</param>
        /// <returns></returns>
        public ConverterRedinessScore GetConverterScore(RegisteredConverter registeredConverter, Type source, Type dest)
        {
            var converterSource = registeredConverter.SourceType;
            var converterDest = registeredConverter.DestType;

            int? sourceDistance = null;
            int? destDistance = null;

            var sourceBreakdown = new TypeBreakDown(source);
            var converterSourceBreakdown = new TypeBreakDown(converterDest);

            if (source == converterSource)
                sourceDistance = 0;
            else if (sourceBreakdown.Interfaces.Contains(converterSource))
                sourceDistance = 1;
            else if (sourceBreakdown.BaseTypes.Contains(converterSource))
                sourceDistance = sourceBreakdown.BaseTypes.IndexOf(converterSource);

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
}