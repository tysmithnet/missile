using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Missile.Core
{
    public interface IHttpService
    {
        Task<string> GetStringAsync(string url);
    }
}
