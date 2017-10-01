using System;
using System.Reflection;

namespace Missile.TextLauncher.Conversion
{
    /// <summary>
    /// Represents a converter that has been registered with a repositorys
    /// </summary>
    public class RegisteredConverter
    {
        /// <summary>
        /// Instance of the converter
        /// </summary>
        public IConverter ConverterInstance { get; set; }

        /// <summary>
        /// Source type of the converter
        /// </summary>
        public Type SourceType { get; set; }

        /// <summary>
        /// Destination type of the converter
        /// </summary>
        public Type DestType { get; set; }

        /// <summary>
        /// Convenience MethodInfo for the converter
        /// </summary>
        public MethodInfo ConvertMethodInfo { get; set; }

        /// <summary>
        /// Convenience method to invoke the converter
        /// </summary>
        /// <param name="source">Instance to convert</param>
        /// <returns>Converted result</returns>
        public object Convert(object source)
        {
            return ConvertMethodInfo.Invoke(ConverterInstance, new[] {source});
        }
    }
}