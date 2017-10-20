using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Windows.Automation;

namespace Missile.IntegrationTests
{
    [ExcludeFromCodeCoverage]
    public sealed class MissileClient : IDisposable
    {
        private readonly Process _process;

        public MissileClient(int waitMs = 1000)
        {
            _process = Process.Start(Path.GetFullPath("../../../Missile.Client/bin/Debug/Missile.Client.exe"));
            Thread.Sleep(waitMs);
            MainWindow = AutomationElement.RootElement.FindFirst(
                TreeScope.Children,
                new PropertyCondition(AutomationElement.AutomationIdProperty,
                    "Missile.Client.MainWindow"));

            InputTextBox = MainWindow.FindFirst(TreeScope.Descendants,
                new PropertyCondition(AutomationElement.AutomationIdProperty,
                    "Missile.TextLauncher.TextLauncherImplementation.InputTextBox"));
        }

        public AutomationElement MainWindow { get; }

        public AutomationElement InputTextBox { get; }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private void ReleaseUnmanagedResources()
        {
            _process.Kill();
        }

        ~MissileClient()
        {
            ReleaseUnmanagedResources();
        }
    }
}