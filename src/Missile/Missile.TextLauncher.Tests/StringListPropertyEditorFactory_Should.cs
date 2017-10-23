using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;
using FluentAssertions;
using Missile.Core;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class StringListPropertyEditorFactory_Should
    {
        private class Person
        {
            public List<string> EmailAddresses { get; set; } = new List<string>
            {
                "abc@def.com"
            };
        }

        [WpfFact]
        public void Returns_The_Right_Kind_Of_Editor()
        {
            var person = new Person();
            new StringListPropertyEditorFactory().GetControl(
                    new PropertyFieldAdapter(typeof(Person).GetProperty("EmailAddresses"), person)).Should()
                .BeOfType<StackPanel>(); // todo: not great
        }

        [Fact]
        public void Say_It_Can_Handle_The_Requested_Type()
        {
            new StringListPropertyEditorFactory().CanHandle(typeof(string)).Should().BeFalse();
            new StringListPropertyEditorFactory().CanHandle(typeof(string[])).Should().BeFalse();
            new StringListPropertyEditorFactory().CanHandle(typeof(IList<string>)).Should()
                .BeFalse(); // this is because IList<string> l = new array[0] is legal
            new StringListPropertyEditorFactory().CanHandle(typeof(List<string>)).Should().BeTrue();
        }
    }
}