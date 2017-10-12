using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using CommandLine;
using Missile.TextLauncher.Provision;

namespace Missile.TextLauncher.EverythingPlugin
{
    /// <inheritdoc />
    /// <summary>
    ///     Provider that is capable of calling es.exe and returning its results
    /// </summary>
    /// <seealso cref="Missile.TextLauncher.Provision.IProvider{System.IO.FileInfo}" />
    [Export(typeof(IProvider))]
    public class EverythingProvider : IProvider<FileInfo>
    {
        /// <summary>
        ///     Gets or sets the settings repository
        /// </summary>
        /// <value>
        ///     The settings repository
        /// </value>
        [Import]
        protected internal ISettingsRepository SettingsRepository { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name
        /// </summary>
        /// <value>
        ///     The name for this provider
        /// </value>
        public string Name { get; set; } = "everything";

        /// <inheritdoc />
        /// <summary>
        ///     Gets an observable sequence of FileInfo from es.exe
        /// </summary>
        /// <param name="args">Arguments for this provider</param>
        /// <returns>
        ///     The observable sequence of values this provider provides
        /// </returns>
        /// <exception cref="T:System.ArgumentException">args</exception>
        public IObservable<FileInfo> Provide(string[] args)
        {
            if (args == null || args.Length == 0)
                throw new ArgumentException($"{nameof(args)} should have at least 1 element");

            var obs = Observable.Create<FileInfo>(async (observer, token) =>
            {
                var settings = SettingsRepository.Get<EverythingProviderSettings>();
                var commandArgs = args.Take(args.Length - 1).ToArray();
                var options = new EverythingProviderOptions();
                Parser.Default.ParseArgumentsStrict(commandArgs, options); // todo: handle bad args
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = settings.EverythingCommandLineExePath,
                        Arguments = GetEverythingCommandLineArgs(settings, options, args.Last()),
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                process.Start();
                while (!process.HasExited)
                    observer.OnNext(new FileInfo(await process.StandardOutput.ReadLineAsync()));
                process.WaitForExit(1000);
                observer.OnCompleted();
            });
            return obs.Publish().RefCount();
        }

        /// <summary>
        ///     Gets the everything command line arguments
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="options">The options</param>
        /// <param name="search">The search</param>
        /// <returns></returns>
        private static string GetEverythingCommandLineArgs(EverythingProviderSettings settings,
            EverythingProviderOptions options, string search)
        {
            var builder = new EverythingCommandLineArgsBuilder(search)
                .WithMaxNumberResults(options.NumMaxResults ?? settings.DefaultNumMaxResults);

            // todo: handle the rest
            return builder.Build();
        }
    }
}