using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public interface IRequiresSetup
    {
        Task SetupAsync(CancellationToken cancellationToken);
    }
}
