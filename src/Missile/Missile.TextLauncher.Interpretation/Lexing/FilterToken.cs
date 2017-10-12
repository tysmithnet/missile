namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    /// Token for filters
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.Token" />
    public class FilterToken : Token
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Missile.TextLauncher.Interpretation.Lexing.FilterToken" /> class.
        /// </summary>
        /// <param name="part">The part.</param>
        /// <param name="args">The arguments.</param>
        public FilterToken(string part, string[] args) : base(part, args)
        {
        }
    }
}