using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Missile.TextLauncher.Conversion;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class ConverterSelectionStrategy_Should
    {
        private void CalculateScoreShortcut(Type requestSource, Type converterSource, Type converterDest,
            Type requestDest,
            int sourceDistance, int destDistance, string because)
        {
            var converterSelectionStrategy = new ConverterSelectionStrategy();
            converterSelectionStrategy.GetConverterScore(new RegisteredConverter
                {
                    SourceType = converterSource,
                    DestType = converterDest
                }, requestSource, requestDest).Should()
                .Be(new ConverterRedinessScore {SourceDistance = sourceDistance, DestDistance = destDistance}, because);
        }

        [Fact]
        public void Calculate_Correct_Scores()
        {
            CalculateScoreShortcut(typeof(Dog), typeof(Dog), typeof(Dog), typeof(Dog), 0, 0,
                "exact match should be 0,0");
            CalculateScoreShortcut(typeof(Corgi), typeof(Dog), typeof(Dog), typeof(Dog), 1, 0,
                "corgis are a direct subclass of dog");
            CalculateScoreShortcut(typeof(Corgi), typeof(Dog), typeof(Corgi), typeof(Dog), 1, 1,
                "corgis are a direct subclass of dog");
            CalculateScoreShortcut(typeof(Dog), typeof(Dog), typeof(Corgi), typeof(Dog), 0, 1,
                "corgis are a direct subclass of dog");
            CalculateScoreShortcut(typeof(IQuadraped), typeof(IHasLegs), typeof(Snake), typeof(INoLegs), 1, 1,
                "quadraped is 1 from haslegs and snake is 1 from nolegs");
            CalculateScoreShortcut(typeof(Chimp), typeof(Mammal), typeof(Python), typeof(Amphibion), 2, 2,
                "a chimp is 2 from mammal and python is 2 from amphibion");
        }

        [Fact]
        public void Return_Null_If_Not_A_Match()
        {
            var converterSelectionStrategy = new ConverterSelectionStrategy();
            converterSelectionStrategy.GetConverterScore(new RegisteredConverter
                {
                    SourceType = typeof(int),
                    DestType = typeof(int)
                }, typeof(string), typeof(string)).Should()
                .Be(null, "there is no compatible conversion between string and int");
        }
    }
}