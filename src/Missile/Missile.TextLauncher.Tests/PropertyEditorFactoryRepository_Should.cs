using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class PropertyEditorFactoryRepository_Should
    {
        [Fact]
        public void Return_The_First_Editor_Capable_Of_Handling_Request()
        {
            var repo = new PropertyEditorFactoryRepository();
            repo.PropertyEditorFactories = new IPropertyEditorFactory[]
                {new StringListPropertyEditorFactory(), new StringPropertyEditorFactory()};
            repo.Get(typeof(string)).Should().BeOfType<StringPropertyEditorFactory>();
        }

        [Fact]
        public void Throw_If_No_Match()
        {
            var repo = new PropertyEditorFactoryRepository();
            repo.PropertyEditorFactories = new IPropertyEditorFactory[]
                {new StringListPropertyEditorFactory(), new StringPropertyEditorFactory()};
            repo.Invoking(repository => repository.Get(typeof(int))).ShouldThrow<ArgumentOutOfRangeException>();
        }
    }
}