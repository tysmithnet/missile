using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
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
