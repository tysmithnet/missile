using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using CommandLine;

namespace Missile.TextLauncher.Filtration
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a filter that will only provide up to a maximum number of values
    /// </summary>
    /// <seealso cref="!:Missile.TextLauncher.Filtration.IFilter{System.Object, System.Object}" />
    [Export(typeof(IFilter<object, object>))]
    public class HeadFilter : IFilter<object, object>
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public string Name { get; set; } = "head";

        /// <summary>
        ///     Filters the specified source.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="source">The source.</param>
        /// <returns>
        ///     The filtered source
        /// </returns>
        /// <inheritdoc />
        public IObservable<object> Filter(string[] args, IObservable<object> source)
        {
            var options = new HeadFilterOptions();
            Parser.Default.ParseArgumentsStrict(args, options);
            // todo: failure
            return source.Take(options.Number);
        }
    }

    public class HeadFilterOptions
    {
        [Option('n', "number", HelpText = "How many to take from the start", DefaultValue = 10)]
        public int Number { get; set; }
    }
}