namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    ///     State representing the start of collecting any arguments for a destination
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.PrimaryArgState" />
    internal class DestinationArgState : PrimaryArgState
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Missile.TextLauncher.Interpretation.Lexing.DestinationArgState" />
        ///     class.
        /// </summary>
        /// <param name="identifier">The identifier</param>
        public DestinationArgState(string identifier) : base(identifier)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the token for this provider, filter, desintation
        /// </summary>
        /// <returns>The token for this provider, filter, destination</returns>
        public override Token GetToken()
        {
            return new DestinationToken(Identifier, Args.ToArray());
        }
    }
}