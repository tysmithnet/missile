namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    ///     Token for any operation between providers, filters, and destinations
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.Token" />
    public class OperatorToken : Token
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Missile.TextLauncher.Interpretation.Lexing.OperatorToken" /> class.
        /// </summary>
        /// <param name="part">The part.</param>
        /// <param name="args">The arguments.</param>
        public OperatorToken(string part, string[] args) : base(part, args)
        {
        }
    }
}