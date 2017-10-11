using CommandLine;

namespace Missile.TextLauncher.Provision.RandomValue
{
    public class RandomValueProviderOptions
    {
        [VerbOption("int", HelpText = "Generate integers")]
        public RandomValueProviderIntOptions RandomValueProviderIntOptions { get; set; }

        [VerbOption("lorem", HelpText = "Generate values from the classic lorem ipsum nonsense")]
        public RandomValueProviderLoremOptions RandomValueProviderLoremOptions { get; set; }
    }
}