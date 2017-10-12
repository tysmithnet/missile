namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <summary>
    /// State representing the start of finding a destination
    /// </summary>
    /// <seealso cref="Missile.TextLauncher.Interpretation.Lexing.PrimaryState" />
    internal class DestinationState : PrimaryState
    {
        /// <summary>
        /// Gets the token
        /// </summary>
        /// <returns>The token</returns>
        public override Token GetToken()
        {
            return new DestinationToken(Identifier, new string[0]);
        }

        /// <summary>
        /// Gets the argument state for this pipeline component
        /// </summary>
        /// <returns>The </returns>
        public override PrimaryArgState GetArgState()
        {
            return new DestinationArgState(Identifier);
        }
    }
}