using System.Linq;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    /// <summary>
    ///     Represents a node of the abstract syntax tree created by the parser
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        ///     Gets or sets the name of the node
        /// </summary>
        /// <value>
        ///     The name
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the arguments
        /// </summary>
        /// <value>
        ///     The arguments
        /// </value>
        public string[] Args { get; set; } = new string[0];

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var node = obj as Node;
            return node != null &&
                   Name == node.Name &&
                   Args.SequenceEqual(node.Args);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
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