﻿using System.Linq;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    public abstract class Node
    {
        public string Name { get; set; }
        public string[] Args { get; set; } = new string[0];

        public override bool Equals(object obj)
        {
            var node = obj as Node;
            return node != null &&
                   Name == node.Name &&
                   Args.SequenceEqual(node.Args);
        }

        public override int GetHashCode()
        {
            var hashCode = -1319852120;
            hashCode ^= Name.GetHashCode();
            foreach (var arg in Args)
                hashCode ^= arg.GetHashCode();
            return hashCode;
        }
    }
}