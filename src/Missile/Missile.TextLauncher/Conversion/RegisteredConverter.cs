﻿using System;
using System.Reflection;

namespace Missile.TextLauncher.Conversion
{
    public class RegisteredConverter
    {
        public IConverter ConverterInstance { get; set; }
        public Type SourceType { get; set; }
        public Type DestType { get; set; }
        public MethodInfo ConvertMethodInfo { get; set; }

        public object Convert(object source)
        {
            return ConvertMethodInfo.Invoke(ConverterInstance, new[] {source});
        }
    }
}