using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Controls;
using FluentAssertions;
using Moq;
using Xunit;

namespace Missile.TextLauncher.ListPlugin.Tests
{
    [ExcludeFromCodeCoverage]
    public class ListDestinationOutputViewModel_Should
    {
        [WpfFact]
        public void Update_The_MenuItems()
        {
            var input = new IListDestinationItem[]
            {
                new StringListDestinationItem(),
                new StringListDestinationItem()
            }.ToList();
            var contextMenuProvider = new Mock<IDestinationContextMenuProvider>();
            contextMenuProvider.Setup(provider => provider.GetMenuItems(input)).Returns(new List<MenuItem>
            {
                new MenuItem(),
                new MenuItem()
            });
            var vm = new ListDestinationOutputViewModel(input.ToObservable(), new[] {contextMenuProvider.Object});
            vm.PopulateMenuItems(input);
            vm.MenuItems.Count.Should().Be(2);
        }
    }
}