using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.IntegrationTests
{
    public class Startup_Should
    {
        [Fact]
        public void Give_InputTextBox_Focus()
        {
            using (var client = new MissileClient())
            {
                // note: this is tough to debug because the debugger can take focus before the
                // property value is captured
                client.InputTextBox.Current.HasKeyboardFocus.Should()
                    .BeTrue("the main text box should have focus on startup");
            }
        }

        [Fact]
        public void Load_In_Under_2_Second()
        {
            using (var client = new MissileClient(2000))
            {
                client.MainWindow.Should().NotBeNull("the main window should always show");
            }
        }
    }
}