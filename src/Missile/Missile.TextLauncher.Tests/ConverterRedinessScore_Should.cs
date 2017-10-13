using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Missile.TextLauncher.Conversion;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class ConverterRedinessScore_Should
    {
        [Fact]
        public void Provide_Correct_Equality_Logic()
        {
            new ConverterRedinessScore
            {
                DestDistance = 1,
                SourceDistance = 2
            }.Equals(new ConverterRedinessScore
            {
                DestDistance = 1,
                SourceDistance = 2
            }).Should().BeTrue();

            new ConverterRedinessScore
            {
                DestDistance = 1,
                SourceDistance = 2
            }.Equals(new ConverterRedinessScore
            {
                DestDistance = 2,
                SourceDistance = 1
            }).Should().BeFalse();

            new ConverterRedinessScore
            {
                DestDistance = 1,
                SourceDistance = 2
            }.GetHashCode().Equals(new ConverterRedinessScore
            {
                DestDistance = 1,
                SourceDistance = 2
            }.GetHashCode()).Should().BeTrue();

            new ConverterRedinessScore
            {
                DestDistance = 1,
                SourceDistance = 2
            }.GetHashCode().Equals(new ConverterRedinessScore
            {
                DestDistance = 2,
                SourceDistance = 1
            }.GetHashCode()).Should().BeFalse();
        }
    }
}