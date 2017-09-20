using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.Core
{
    public interface ICacheFacade
    {
        ICache<TKey, TResult> Get<TKey, TResult>();
    }
}
