using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Linq;
using CommandLine;

namespace Missile.TextLauncher.Provision.Range
{
    /// <inheritdoc />
    /// <summary>
    ///     Provider that provides a range of values
    /// </summary>
    /// <seealso cref="T:System.Object" />
    [Export(typeof(IProvider))]
    public class RangeProvider : IProvider<object>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name of this provider
        /// </summary>
        /// <value>
        ///     The name for this provider
        /// </value>
        public string Name { get; set; } = "range";

        /// <inheritdoc />
        /// <summary>
        ///     Gets the observable sequence of range values
        /// </summary>
        /// <param name="args">Arguments for this provider</param>
        /// <returns>
        ///     The observable sequence of range values
        /// </returns>
        public IObservable<object> Provide(string[] args)
        {
            var options = new RangeProviderOptions();
            Parser.Default.ParseArgumentsStrict(args, options);
            return Generate(options).Cast<object>().ToObservable();
        }

        /// <summary>
        ///     Generates the range of values this provider will provide
        /// </summary>
        /// <param name="options">The options governing this providers behavior</param>
        /// <returns>An enumeration of values dictated by the provided options</returns>
        private IEnumerable<int> Generate(RangeProviderOptions options)
        {
            for (var i = options.Start; i < options.End; i += options.Increment)
                yield return i;
        }
    }
}