using System;
using System.Linq;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <summary>
    ///     Lexers produce streams of tokens to be parsed
    /// </summary>
    public abstract class Token
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Token" /> class.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="args">The arguments.</param>
        /// <exception cref="ArgumentNullException">input</exception>
        protected internal Token(string input, string[] args)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            Name = input;
            Args = args ?? new string[0];
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        protected internal string Name { get; set; }

        /// <summary>
        ///     Gets or sets the arguments.
        /// </summary>
        /// <value>
        ///     The arguments.
        /// </value>
        protected internal string[] Args { get; set; }

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var token = obj as Token;
            return token != null &&
                   GetType() == token.GetType() &&
                   Name == token.Name &&
                   Args.SequenceEqual(token.Args);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            var hashCode = 824443;
            if (Name != null)
                hashCode ^= Name.GetHashCode() % 820921;
            foreach (var arg in Args)
                hashCode ^= arg.GetHashCode() % 373903;

            return hashCode;
        }
    }
}