using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using CommandLine;

namespace Missile.TextLauncher.Provision
{
    public class RandomValueProvider : IProvider<object>
    {
        private static readonly string Lorem =
                @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis hendrerit tortor ut varius euismod. Nullam ut nisi ultrices, mattis mi at, fermentum odio. Duis varius, lorem et pharetra finibus, purus tortor pellentesque risus, a blandit eros nibh et ligula. Vestibulum scelerisque malesuada magna, a porttitor mi. Maecenas cursus lorem nec lectus venenatis sagittis. Sed ultricies libero at ligula porttitor, nec pellentesque libero sagittis. Sed efficitur dictum mauris vitae mollis. Nulla lacus eros, vulputate id rhoncus ut, malesuada ac nunc. Sed id arcu fermentum, molestie turpis fermentum, finibus ipsum. Nam eu diam sagittis libero interdum pellentesque vel eget libero. Suspendisse interdum accumsan turpis, a laoreet augue eleifend eget. Mauris sit amet est malesuada felis auctor scelerisque. Sed quis tincidunt nibh.
Interdum et malesuada fames ac ante ipsum primis in faucibus. Praesent eu vehicula justo. In vel tincidunt elit. Cras eget condimentum augue. Curabitur non pretium erat. Quisque sit amet scelerisque massa. Morbi sit amet mattis tortor. Cras et nisi sem. Cras vitae metus hendrerit, ultrices massa a, gravida diam. Suspendisse iaculis luctus nunc vitae maximus. Suspendisse luctus velit a dictum faucibus. Nullam tellus lectus, maximus id urna nec, lacinia vehicula ante. Maecenas nisl turpis, tincidunt at ex malesuada, cursus blandit erat. Pellentesque rhoncus, quam ac elementum mollis, eros ipsum mattis purus, nec rutrum odio nisi at neque.
Sed placerat orci nec urna tristique, in dapibus libero sollicitudin. Nullam in rhoncus lorem, et suscipit nisl. Nam condimentum auctor lacus, eu tincidunt risus rhoncus ut. Duis lacinia lacus mauris, non interdum magna gravida et. Proin consectetur tincidunt risus, eu interdum orci aliquam interdum. Quisque auctor, neque sit amet accumsan porttitor, mi nisl auctor sapien, vel venenatis mauris dui in nunc. Suspendisse facilisis sapien at metus pretium, vehicula dapibus dolor hendrerit. Proin porttitor felis arcu, ut dignissim ipsum consequat quis. Aliquam eu sem consectetur, sollicitudin sapien vel, varius risus. Nunc efficitur massa diam, vitae porttitor arcu pellentesque in. Nullam et pellentesque dui, non aliquet lorem. Etiam auctor eleifend dolor, ut faucibus nisl vestibulum vel. Maecenas ut lacinia velit.
In suscipit felis sed ullamcorper accumsan. Morbi ultrices euismod tempus. Cras non facilisis felis. Nam sit amet arcu facilisis, cursus libero sit amet, gravida lacus. Fusce mi quam, sollicitudin at faucibus imperdiet, porta vitae arcu. Nam cursus dolor imperdiet, laoreet orci a, pharetra nisl. Maecenas lacus urna, sollicitudin a turpis pellentesque, tempor blandit purus. Sed non leo quis sapien imperdiet ultrices. Mauris non tincidunt justo.
Vivamus blandit turpis in risus condimentum consequat. In tortor sapien, pharetra vitae blandit non, volutpat at turpis. Curabitur vitae ante ante. Nunc eu bibendum velit. Fusce eget consequat augue. Etiam scelerisque dui tortor, ut dictum dui imperdiet vitae. Etiam tempor est nec malesuada congue."
            ;

        private static readonly string[] Words = Regex.Split(Lorem, @"\s+");
        private static readonly string[] Sentences = Regex.Split(Lorem, @"\.");

        static RandomValueProvider()
        {
        }

        public string Name { get; set; } = "random";

        public IObservable<object> Provide(string[] args)
        {
            IObservable<object> result = null;
            var options = new Options();
            Parser.Default.ParseArgumentsStrict(args, options, (verb, subOptions) =>
            {
                var baseOptions = subOptions as BaseOptions;
                Debug.Assert(baseOptions != null, $"{nameof(subOptions)} cannot be null");
                var rng = new Random();
                if (baseOptions.Seed.HasValue)
                    rng = new Random(baseOptions.Seed.Value);
                switch (verb)
                {
                    case "int":
                        var intOptions = subOptions as IntOptions;
                        result = Enumerable.Range(0, baseOptions.Count)
                            .Select(x => rng.Next(intOptions.Min, intOptions.Max)).Cast<object>()
                            .ToObservable();
                        break;
                    case "lorem":
                        var loremOptions = subOptions as LoremOptions;

                        switch (loremOptions.Type)
                        {
                            case "word":
                                result = Enumerable.Range(0, loremOptions.Count)
                                    .Select(x => Words[rng.Next(0, Words.Length)])
                                    .ToObservable();
                                break;
                            case "sentence":
                                result = Enumerable.Range(0, loremOptions.Count)
                                    .Select(x => Sentences[rng.Next(0, Words.Length)]).ToObservable();
                                break;
                        }

                        break;
                }
            }, () => throw new ArgumentException());

            return result ?? throw new ArgumentException();
        }

        private sealed class Options
        {
            [VerbOption("int", HelpText = "Generate integers")]
            public IntOptions IntOptions { get; set; }

            [VerbOption("lorem", HelpText = "Generate values from the classic lorem ipsum nonsense")]
            public LoremOptions LoremOptions { get; set; }
        }

        private class BaseOptions
        {
            [Option('c', "count", DefaultValue = 5, HelpText = "How many values to produce")]
            public int Count { get; set; }

            [Option('s', "seed", HelpText = "Seed value for random number generator", Required = false)]
            public int? Seed { get; set; }
        }

        private class IntOptions : BaseOptions
        {
            [Option('m', "min", DefaultValue = 0, HelpText = "The lowest number cabable of being produced (inclusive)",
                Required = false)]
            public int Min { get; set; }

            [Option('x', "max", DefaultValue = 0, HelpText = "The highest number cabable of being produced (exclusive)",
                Required = false)]
            public int Max { get; set; }
        }

        private class LoremOptions : BaseOptions
        {
            [Option('t', "type", DefaultValue = "word",
                HelpText = "The type of value to produce (word, sentence, paragraph)", Required = false)]
            public string Type { get; set; }
        }
    }
}