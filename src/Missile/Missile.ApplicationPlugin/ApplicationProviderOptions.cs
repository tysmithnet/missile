using System.Collections.Generic;
using CommandLine;

namespace Missile.ApplicationPlugin
{
    /// <summary>
    ///     Options for ApplicationProvider
    /// </summary>
    public class ApplicationProviderOptions
    {
        /// <summary>
        ///     Gets or sets the search strings
        /// </summary>
        /// <value>
        ///     The search strings
        /// </value>
        [ValueList(typeof(List<string>))]
        public IList<string> SearchStrings { get; set; }
    }
}