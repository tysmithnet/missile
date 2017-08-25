using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Missile.Core
{
    public interface IConfigurationService
    {
        Task<T> GetConfigAsync<T>(string provider);
    }
}
