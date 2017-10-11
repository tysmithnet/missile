using CommandLine;

namespace Missile.TextLauncher.Provision.RandomValue
{
    public class RandomValueProviderLoremOptions : RandomValueProviderBaseOptions
    {
        [Option('t', "type", DefaultValue = "word",
            HelpText = "The type of value to produce (word, sentence, paragraph)", Required = false)]
        public string Type { get; set; }
    }
}