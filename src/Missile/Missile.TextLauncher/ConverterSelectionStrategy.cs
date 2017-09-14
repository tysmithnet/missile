using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher
{
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
}