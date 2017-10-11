using CommandLine;

namespace Missile.TextLauncher.Provision.RandomValue
{
    public class RandomValueProviderBaseOptions
    {
        [Option('c', "count", DefaultValue = 5, HelpText = "How many values to produce")]
        public int Count { get; set; }

        [Option('s', "seed", HelpText = "Seed value for random number generator", Required = false)]
        public int? Seed { get; set; }
    }
}