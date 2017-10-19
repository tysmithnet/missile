using System.Text;

namespace Missile.TextLauncher.EverythingPlugin
{
    /// <summary>
    ///     Builder for creating command line arguments to es.exe
    /// </summary>
    public class EverythingCommandLineArgsBuilder
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EverythingCommandLineArgsBuilder" /> class
        /// </summary>
        public EverythingCommandLineArgsBuilder()
        {
            StringBuilder = new StringBuilder();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="T:Missile.TextLauncher.EverythingPlugin.EverythingCommandLineArgsBuilder" /> class
        /// </summary>
        /// <param name="search">The search</param>
        public EverythingCommandLineArgsBuilder(string search) : this()
        {
            SearchString = search;
        }

        /// <summary>
        ///     Gets or sets the string builder
        /// </summary>
        /// <value>
        ///     The string builder
        /// </value>
        protected internal StringBuilder StringBuilder { get; set; }

        /// <summary>
        ///     Gets or sets the search string
        /// </summary>
        /// <value>
        ///     The search string
        /// </value>
        protected internal string SearchString { get; set; }

        /// <summary>
        ///     Sets the maximum number results
        /// </summary>
        /// <param name="max">The maximum</param>
        /// <returns>this builder</returns>
        public EverythingCommandLineArgsBuilder WithMaxNumberResults(int max)
        {
            StringBuilder.Append($" -n {max}");
            return this;
        }

        /// <summary>
        ///     Sets the posix regex
        /// </summary>
        /// <returns>this builder</returns>
        public EverythingCommandLineArgsBuilder WithPosixRegex()
        {
            StringBuilder.Append(" -r");
            return this;
        }

        /// <summary>
        ///     Withes the whole word search
        /// </summary>
        /// <returns>this builder</returns>
        public EverythingCommandLineArgsBuilder WithWholeWordSearch()
        {
            StringBuilder.Append(" -w");
            return this;
        }

        /// <summary>
        ///     Withes the case insensitive search
        /// </summary>
        /// <returns>this builder</returns>
        public EverythingCommandLineArgsBuilder WithCaseInsensitiveSearch()
        {
            StringBuilder.Append(" -i");
            return this;
        }

        /// <summary>
        ///     Withes the full path search
        /// </summary>
        /// <returns>this builder</returns>
        public EverythingCommandLineArgsBuilder WithFullPathSearch()
        {
            StringBuilder.Append(" -p");
            return this;
        }

        /// <summary>
        ///     Withes the sort by full path
        /// </summary>
        /// <returns>this builder</returns>
        public EverythingCommandLineArgsBuilder WithSortByFullPath()
        {
            StringBuilder.Append(" -s");
            return this;
        }

        /// <summary>
        ///     Withes the search string
        /// </summary>
        /// <param name="search">The search</param>
        /// <returns>this builder</returns>
        public EverythingCommandLineArgsBuilder WithSearchString(string search)
        {
            SearchString = search;
            return this;
        }

        /// <summary>
        ///     Builds this instance
        /// </summary>
        /// <returns>Command line arguments to es.exe</returns>
        public string Build()
        {
            var s = StringBuilder.Length == 0 ? StringBuilder + SearchString : StringBuilder + " " + SearchString;
            return s.TrimStart();
        }
    }
}