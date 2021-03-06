﻿using System;

namespace Missile.TextLauncher.Conversion
{
    /// <summary>
    ///     Tagging interface for Converters
    /// </summary>
    public interface IConverter
    {
    }

    /// <inheritdoc />
    /// <summary>
    ///     Represents an object that is capable of converting oberservables
    ///     of one type into observables of another type
    /// </summary>
    /// <typeparam name="TSource">Source observable type</typeparam>
    /// <typeparam name="TDest">Destination observable type</typeparam>
    public interface IConverter<in TSource, out TDest> : IConverter
    {
        /// <summary>
        ///     Converts the specified source
        /// </summary>
        /// <param name="source">The source</param>
        /// <returns></returns>
        IObservable<TDest> Convert(IObservable<TSource> source);
    }
}