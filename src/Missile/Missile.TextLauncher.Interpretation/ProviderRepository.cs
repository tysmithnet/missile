using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public class ProviderRepository : IProviderRepository
    {
        public IList<RegisteredProvider> RegisteredProviders { get; set; }

        public RegisteredProvider Get(string providerName)
        {
            return RegisteredProviders.Single(x => x.Name == providerName);
        }
    }
}
