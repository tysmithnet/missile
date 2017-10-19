using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
        [WpfFact]
        public void Convert_Strings_To_TextBlocks()
        {
            var converter = new StringFrameworkElementConverter();
            var obs = Observable.Range(0, 3).Select(x => x.ToString());
            var list = converter.Convert(obs).ToEnumerable().ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var x = list[i];
                var tb = x as TextBlock;
                tb.Should().NotBeNull();
                tb.Text.Should().Be($"{i}");
            }
        }
    }
}