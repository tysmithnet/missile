using System;
using System.Collections.Generic;

namespace Missile.TextLauncher.Conversion
{
    /// <summary>
    ///     Represents a central location for managing converters
    /// </summary>
    public interface IConverterRepository
    {
        /// <summary>
        ///     Gets a converter for the conversion from source -> dest
        /// </summary>
        /// <param name="source">Source type of the conversion</param>
        /// <param name="dest">Destination type of the conversion</param>
        /// <returns>A suitable RegisteredConverter for the conversion of source -> dest</returns>
        /// <exception cref="KeyNotFoundException">There was no suitable converter for the requested conversion</exception>
        RegisteredConverter Get(Type source, Type dest);

        /// <summary>
        ///     Registers a new converter with this repository
        /// </summary>
        /// <param name="registeredConverter">Converter to register</param>
        /// <exception cref="ArgumentNullException">Converter is null</exception>
        void Register(RegisteredConverter registeredConverter);
    }
}