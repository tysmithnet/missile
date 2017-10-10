using System.Text;

namespace Missile.TextLauncher.EverythingPlugin
{
    public class EverythingCommandLineArgsBuilder
    {
        public EverythingCommandLineArgsBuilder()
        {
            StringBuilder = new StringBuilder();
        }

        public EverythingCommandLineArgsBuilder(string search) : this()
        {
            SearchString = search;
        }

        protected internal StringBuilder StringBuilder { get; set; }
        protected internal string SearchString { get; set; }

        public EverythingCommandLineArgsBuilder WithMaxNumberResults(int max)
        {
            StringBuilder.Append($" -n {max}");
            return this;
        }

        public EverythingCommandLineArgsBuilder WithPosixRegex()
        {
            StringBuilder.Append(" -r");
            return this;
        }

        public EverythingCommandLineArgsBuilder WithWholeWordSearch()
        {
            StringBuilder.Append(" -w");
            return this;
        }

        public EverythingCommandLineArgsBuilder WithCaseInsensitiveSearch()
        {
            StringBuilder.Append(" -i");
            return this;
        }

        public EverythingCommandLineArgsBuilder WithFullPathSearch()
        {
            StringBuilder.Append(" -p");
            return this;
        }

        public EverythingCommandLineArgsBuilder WithSortByFullPath()
        {
            StringBuilder.Append(" -s");
            return this;
        }

        public EverythingCommandLineArgsBuilder WithSearchString(string search)
        {
            SearchString = search;
            return this;
        }

        public string Build()
        {
            StringBuilder.Append($" {SearchString}");
            return StringBuilder.ToString();
        }
    }
}