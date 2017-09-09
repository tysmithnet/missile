using System.Reflection;

namespace Missile.TextLauncher
{
    public sealed class RegisteredFilter
    {
        public string Name { get; set; }
        public object FilterInstance { get; set; }
        public MethodInfo FilterMethodInfo { get; set; }
    }
}