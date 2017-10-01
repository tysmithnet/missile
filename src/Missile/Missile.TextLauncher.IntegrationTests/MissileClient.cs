using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Automation;

namespace Missile.TextLauncher.IntegrationTests
{
    public sealed class MissileClient : IDisposable
    {
        private Process _process;
        private AutomationElement _mainWindow;

        public MissileClient(int waitMs = 1000)
        {
            _process = Process.Start(Path.GetFullPath("../../../Missile.Client/bin/Debug/Missile.Client.exe"));
            Thread.Sleep(waitMs);
            _mainWindow = AutomationElement.RootElement.FindFirst(TreeScope.Children,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "Missile.Client.MainWindow"));
        }

        public AutomationElement GetMainWindow()
        {
            return _mainWindow;
        }

        private void ReleaseUnmanagedResources()
        {
            _process.Kill();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~MissileClient()
        {
            ReleaseUnmanagedResources();
        }
    }
}