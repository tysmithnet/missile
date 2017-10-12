using System;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <summary>
    /// EventArgs for when tokens are emitted from the state machine
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    internal class TokenEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenEventArgs"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <exception cref="ArgumentNullException">token</exception>
        public TokenEventArgs(Token token)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public Token Token { get; set; }
    }
}