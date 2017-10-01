using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.IntegrationTests
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var client = new MissileClient())
            {
                Console.WriteLine("Loaded client successfully");
                Thread.Sleep(2000);
            }
        }
    }

    public sealed class MissileClient : IDisposable
    {
        private Process _process;

        public MissileClient()
        {
            _process = Process.Start(Path.GetFullPath("../../../Missile.Client/bin/Debug/Missile.Client.exe"));
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
