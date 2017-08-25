using System.Collections.Generic;  

namespace Missile.EverythingPlugin
{
    public interface IEverythingAdapter
    {
        List<string> Search(string query);
    }
}