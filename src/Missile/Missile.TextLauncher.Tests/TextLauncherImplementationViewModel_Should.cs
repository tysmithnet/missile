using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using FluentAssertions;
using Missile.Core.Logging;
using Moq;
using Xunit;

namespace Missile.TextLauncher.Tests
{
    [ExcludeFromCodeCoverage]
    public class TextLauncherImplementationViewModel_Should
    {
        [WpfFact]
        public void Execute_Input_As_The_Command()
        {
            var loggerMock = new Mock<ILogger>();
            var facadeMock = new Mock<IInterpretationFacade>();
            var requiresSetup = new[] {new Mock<IRequiresSetup>().Object};
            var launcher =
                new TextLauncherImplementationViewModel(loggerMock.Object, facadeMock.Object, requiresSetup)
                {
                    InputText = "noop"
                };
            launcher.Invoking(async model => await model.ExecuteCommandAsync()).ShouldNotThrow();
        }

        [WpfFact]
        public async Task Execute_Command_When_Enter_Or_Return_Are_Pressed()
        {
            var loggerMock = new Mock<ILogger>();
            var facadeMock = new Mock<IInterpretationFacade>();
            facadeMock.Setup(facade => facade.ExecuteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Throws<FormatException>();
            var requiresSetup = new[] { new Mock<IRequiresSetup>().Object };
            var launcher =
                new TextLauncherImplementationViewModel(loggerMock.Object, facadeMock.Object, requiresSetup)
                {
                    InputText = "#$#@"
                };
            var keyArgs = new KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(0,0,0,0,0,"", IntPtr.Zero), 0, Key.Enter);
            await launcher.HandleInputKeyDownEventAsync(keyArgs);
            launcher.OutputControl.Should().BeOfType<ErrorViewer>();
            launcher.OutputControl = new TextBox();
            keyArgs = new KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero), 0, Key.Return);
            await launcher.HandleInputKeyDownEventAsync(keyArgs);
            launcher.OutputControl.Should().BeOfType<ErrorViewer>();

        }

        [WpfFact]
        public async Task Set_ErrorViewer_As_Output_If_Error()
        {
            var loggerMock = new Mock<ILogger>();
            var facadeMock = new Mock<IInterpretationFacade>();
            facadeMock.Setup(facade => facade.ExecuteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Throws<FormatException>();
            var requiresSetup = new[] { new Mock<IRequiresSetup>().Object };
            var launcher =
                new TextLauncherImplementationViewModel(loggerMock.Object, facadeMock.Object, requiresSetup)
                {
                    InputText = "#$#@"
                };
            await launcher.ExecuteCommandAsync();
            launcher.OutputControl.Should().BeOfType<ErrorViewer>();
        }
    }
}