using CommandLine;

namespace Missile.TextLauncher.Provision.RandomValue
{
    public class RandomValueProviderIntOptions : RandomValueProviderBaseOptions
    {
        [Option('m', "min", DefaultValue = 0, HelpText = "The lowest number cabable of being produced (inclusive)",
            Required = false)]
        public int Min { get; set; }

        [Option('x', "max", DefaultValue = 0, HelpText = "The highest number cabable of being produced (exclusive)",
            Required = false)]
        public int Max { get; set; }
    }
}