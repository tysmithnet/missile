using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation.Compilation
{
    public abstract class Node
    {
        public string Name { get; set; }
        public string ArgString { get; set; }

        public override bool Equals(object obj)
        {
            var node = obj as Node;
            return node != null &&
                   Name == node.Name &&
                   ArgString == node.ArgString;
        }

        public override int GetHashCode()
        {
            var hashCode = -1319852120;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ArgString);
            return hashCode;
        }
    }
}