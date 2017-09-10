using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public interface IConverter
    {
        bool CanHandleRequest(Type sourceType, Type destType);
        object Convert(object source, Type sourceType, Type destType);
    }
}
