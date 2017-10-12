namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <summary>
    /// State representing the start of creating a filter
    /// </summary>
    /// <seealso cref="Missile.TextLauncher.Interpretation.Lexing.PrimaryState" />
    internal class FilterState : PrimaryState
    {
        /// <summary>
        /// Gets the token for this filter
        /// </summary>
        /// <returns>Token for this filter</returns>
        public override Token GetToken()
        {
            return new FilterToken(Identifier, new string[0]);
        }

        /// <summary>
        /// Gets the argument state for this filter
        /// </summary>
        /// <returns></returns>
        public override PrimaryArgState GetArgState()
        {
            return new FilterArgState(Identifier);
        }
    }
}