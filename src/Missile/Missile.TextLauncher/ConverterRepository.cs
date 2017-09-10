using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Missile.TextLauncher
{
    public interface IConverterRepository
    {
        IConverter Get(Type source, Type dest);
    }

    [Export(typeof(IConverterRepository))]
    public class ConverterRepository : IConverterRepository
    {
        [ImportMany(typeof(IConverter))]
        public IEnumerable<IConverter> Converters { get; set; }

        public IConverter Get(Type source, Type dest)
        {
            var converter = Converters.FirstOrDefault(c => c.CanHandleRequest(source, dest));
            if (converter == null)
                throw new ArgumentOutOfRangeException(
                    $"There is no converter registered that can handle the conversion: {source} -> {dest}");
            return converter;
        }
    }
}