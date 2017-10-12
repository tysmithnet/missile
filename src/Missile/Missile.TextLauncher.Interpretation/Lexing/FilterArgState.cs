namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    /// State representing the start of collecting arguments for a filter
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.PrimaryArgState" />
    internal class FilterArgState : PrimaryArgState
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Missile.TextLauncher.Interpretation.Lexing.FilterArgState" /> class
        /// </summary>
        /// <param name="identifier">The identifier</param>
        public FilterArgState(string identifier) : base(identifier)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the token for this filter
        /// </summary>
        /// <returns>Token for this filter</returns>
        public override Token GetToken()
        {
            return new FilterToken(Identifier, Args.ToArray());
        }
    }
}