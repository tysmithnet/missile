using System;
using System.Collections.Generic;
using System.Linq;

namespace Missile.Client.TextLauncher.Compilation
{
    public class ConverterRepository : IConverterRepository
    {
        public IReadOnlyCollection<IConverter> Converters { get; set; }
        
        
        public IConverter Get(Type source, Type dest)
        {
            throw new NotImplementedException();
        }
    }
}