using System.Reflection;

namespace Missile.TextLauncher.Filtration
{
    public sealed class RegisteredFilter
    {
        public string Name { get; set; }
        public object FilterInstance { get; set; }
        public MethodInfo FilterMethodInfo { get; set; }
    }
}