using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Windows.Controls;
using FluentAssertions;
using Missile.TextLauncher.Conversion;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class StringUserControlConverter_Should
    {
        [Fact]
        public void Convert_Strings_To_TextBlocks()
        {
            Utilities.StartSTATask(() =>
            {
                var converter = new StringUserControlConverter();
                converter.Convert(Observable.Range(0, 3).Select(x => x.ToString()))
                    .ToEnumerable().Should().Equal(new TextBlock
                    {
                        Text = "0"
                    }, new TextBlock
                    {
                        Text = "1"
                    }, new TextBlock
                    {
                        Text = "2"
                    });
            });
        }
    }
}