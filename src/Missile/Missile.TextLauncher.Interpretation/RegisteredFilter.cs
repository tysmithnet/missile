using System.Reflection;

namespace Missile.TextLauncher.Interpretation
{
    public class RegisteredFilter
    {
        public string Name { get; set; }
        public object FilterInstance { get; set; }
        public MethodInfo FilterMethodInfo { get; set; }
    }
}