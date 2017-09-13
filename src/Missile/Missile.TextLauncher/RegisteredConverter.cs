using System;
using System.Collections.Generic;
using System.Reflection;

namespace Missile.TextLauncher
{
    public class RegisteredConverter
    {
        public IConverter ConverterInstance { get; set; }
        public Type SourceType { get; set; }
        public List<Type> DestTypes { get; set; }
        public MethodInfo ConvertMethodInfo { get; set; }

        public object Convert(object source)
        {
            return ConvertMethodInfo.Invoke(ConverterInstance, new object[] {source});
        }
    }
}