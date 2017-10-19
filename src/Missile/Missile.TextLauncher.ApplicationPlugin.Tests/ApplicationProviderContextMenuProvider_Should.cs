using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FluentAssertions;
using Missile.Core.FileSystem;
using Missile.TextLauncher.ApplicationPlugin;
using Missile.TextLauncher.ListPlugin;
using Moq;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class ApplicationProviderContextMenuProvider_Should
    {
        [WpfFact]
        public void Provide_Remove_Application_Item_For_Applications()
        {
            var registeredApplication = new RegisteredApplication();
            var applicationListDestinationItem = new ApplicationListDestinationItem(registeredApplication);
            var applicationProviderContextMenuProvider = new ApplicationProviderContextMenuProvider();
            var commandHubMock = new Mock<ICommandHub>();
            applicationProviderContextMenuProvider.CommandHub = commandHubMock.Object;
            var menuItems = applicationProviderContextMenuProvider.GetMenuItems(new[] {applicationListDestinationItem})
                .ToList();
            var first = menuItems.First();
            first.Header.ToString().Should().Be("Remove Application");
            first.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
            commandHubMock.Verify(hub => hub.Broadcast(It.IsAny<ICommand>()), Times.Exactly(3)); // todo: which commands
        }

        [WpfFact]
        public void Provide_Add_Application_Item_For_FileInfo()
        {
            var fsMock = new Mock<IFileSystem>();
            var commandHubMock = new Mock<ICommandHub>();
            var fileListDestinationItem =
                new FileListDestinationItem(new FileInfo("c:\\fake\\path\\file.exe"), fsMock.Object);
            var applicationProviderContextMenuProvider = new ApplicationProviderContextMenuProvider();
            applicationProviderContextMenuProvider.CommandHub = commandHubMock.Object;
            var menuItems = applicationProviderContextMenuProvider.GetMenuItems(new[] {fileListDestinationItem})
                .ToList();
            menuItems.First().Header.ToString().Should().Be("Add Application");
            menuItems.First().RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
            commandHubMock.Verify(hub => hub.Broadcast(It.IsAny<ICommand>()), Times.Exactly(2)); // todo: which commands
        }

        [WpfFact]
        public void Provide_Nothing_If_Invalid_Input()
        {
            var mockDestination = new Mock<IListDestinationItem>();
            var a = new ApplicationProviderContextMenuProvider();
            a.GetMenuItems(new[] {mockDestination.Object}).Should().BeEmpty();
        }
    }
}