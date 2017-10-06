using System;
using System.Collections.Generic;

namespace Missile.TextLauncher.Conversion
{
    /// <summary>
    ///     Represents an object that knows how to select appropriate converters for a given conversion request
    /// </summary>
    public interface IConverterSelectionStrategy
    {
        /// <summary>
        ///     Gets an enumeration of appropriate converters for a given conversion request
        /// </summary>
        /// <param name="registeredConverters">All possible converters</param>
        /// <param name="source">
        ///     Conversion source type (e.g. if you have an IObservable<string> then source would be string
        /// </param>
        /// <param name="dest">
        ///     Conversion destination type (e.g. if you have an IObservable<string> then dest would be string
        /// </param>
        /// <returns>All suitable converters for the requested conversion</returns>
        IEnumerable<RegisteredConverter> Select(IEnumerable<RegisteredConverter> registeredConverters, Type source,
            Type dest);
    }
}