using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using CommandLine;
using Missile.TextLauncher.Provision;

namespace Missile.ApplicationPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     A provider that provides observable sequences of registered applications
    /// </summary>
    /// <seealso
    ///     cref="!:Missile.TextLauncher.Provision.IProvider{Missile.TextLauncher.ApplicationPlugin.RegisteredApplication}" />
    [Export(typeof(IProvider))]
    [Export(typeof(ApplicationProvider))]
    public class ApplicationProvider : IProvider<RegisteredApplication>
    {
        /// <summary>
        ///     Gets or sets the application repository
        /// </summary>
        /// <value>
        ///     The application repository
        /// </value>
        [Import]
        protected internal IApplicationRepository ApplicationRepository { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name of this provider
        /// </summary>
        /// <value>
        ///     The name for this provider
        /// </value>
        [ExcludeFromCodeCoverage]
        public string Name { get; set; } = "apps";

        /// <inheritdoc />
        /// <summary>
        ///     Gets an observable sequence of registered applications based on the provided arguments
        /// </summary>
        /// <param name="args">Arguments for this provider</param>
        /// <returns>
        ///     An observable sequence of registered applications predicated on the provided arguments
        /// </returns>
        public IObservable<RegisteredApplication> Provide(string[] args)
        {
            var options = new ApplicationProviderOptions();
            Parser.Default.ParseArgumentsStrict(args, options);
            return args.SelectMany(x => ApplicationRepository.Search(x))
                .ToObservable();
        }
    }
}