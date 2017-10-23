using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using FluentAssertions;
using Missile.Core;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class StringPropertyEditorFactory_Should
    {
        private class Person
        {
            public string Name { get; set; }
            public string Alias { get; set; }
            public int Age { get; set; }
        }

        [WpfFact]
        public void Return_A_TextBox()
        {
            var stringPropertyEditorFactory = new StringPropertyEditorFactory();
            stringPropertyEditorFactory.GetControl(new PropertyFieldAdapter(typeof(Person).GetProperty("Name"),
                new Person())).Should().BeOfType(typeof(TextBox));
            var person = new Person();
            var control = stringPropertyEditorFactory.GetControl(new PropertyFieldAdapter(
                typeof(Person).GetProperty("Name"),
                person));
            if (control is TextBox textBox)
                textBox.Text = "a";

            var control2 = stringPropertyEditorFactory.GetControl(new PropertyFieldAdapter(
                typeof(Person).GetProperty("Alias"),
                person));
            if (control2 is TextBox textBox2)
                textBox2.Text = "a";
            person.Name.Should().Be("a");
            person.Alias.Should().Be("a");
        }

        [Fact]
        public void Indicate_It_CanHandle_Strings()
        {
            var stringPropertyEditorFactory = new StringPropertyEditorFactory();
            stringPropertyEditorFactory.CanHandle(typeof(string)).Should().BeTrue();
        }
    }
}