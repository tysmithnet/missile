using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using FluentAssertions;
using Missile.Core.FileSystem;
using Moq;
using Xunit;

namespace Missile.TextLauncher.ListPlugin.Tests
{
    [ExcludeFromCodeCoverage]
    public class ListDestination_Should
    {
        [WpfFact]
        public async Task Display_The_Items()
        {
            var fsMock = new Mock<IFileSystem>();
            var uiMock = new Mock<IUiFacade>();
            var hubMock = new Mock<ICommandHub>();
            var items = new List<IListDestinationItem>
            {
                new FileListDestinationItem(new FileInfo("c:\\fake.txt"), fsMock.Object),
                new FileListDestinationItem(new FileInfo("c:\\fake2.txt"), fsMock.Object)
            };
            var a = new[] {new RemoveListDestinationItemCommand(items[0])};
            hubMock.Setup(hub => hub.Get<RemoveListDestinationItemCommand>()).Returns(a.ToObservable());

            var listDestination = new ListDestination
            {
                UiFacade = uiMock.Object,
                ContextMenuProviders = new IDestinationContextMenuProvider[0],
                CommandHub = hubMock.Object
            };
            await listDestination.ProcessAsync(items.ToObservable(), CancellationToken.None);
            uiMock.Verify(facade => facade.SetOutputControl(It.IsAny<FrameworkElement>()), Times.Once);
        }

        [WpfFact]
        public void Throw_If_Observable_Errors()
        {
            var fsMock = new Mock<IFileSystem>();
            var uiMock = new Mock<IUiFacade>();
            var hubMock = new Mock<ICommandHub>();

            var subject = new ReplaySubject<IListDestinationItem>();
            subject.OnError(new FormatException());

            hubMock.Setup(hub => hub.Get<RemoveListDestinationItemCommand>()).Returns(new RemoveListDestinationItemCommand[0].ToObservable());

            var listDestination = new ListDestination
            {
                UiFacade = uiMock.Object,
                ContextMenuProviders = new IDestinationContextMenuProvider[0],
                CommandHub = hubMock.Object
            };
            listDestination.Invoking(destination => destination.ProcessAsync(subject, CancellationToken.None).Wait())
                .ShouldThrow<FormatException>();
        }
    }
}