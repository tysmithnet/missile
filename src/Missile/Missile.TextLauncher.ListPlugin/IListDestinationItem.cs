using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.ListPlugin
{
    public interface IListDestinationItem
    {
        Guid Id { get; }
    }
}
