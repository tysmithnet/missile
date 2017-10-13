namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <summary>
    ///     Token for destinations
    /// </summary>
    /// <seealso cref="Missile.TextLauncher.Interpretation.Lexing.Token" />
    public class DestinationToken : Token
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DestinationToken" /> class.
        /// </summary>
        /// <param name="part">The part.</param>
        /// <param name="args">The arguments.</param>
        public DestinationToken(string part, string[] args) : base(part, args)
        {
        }
    }
}