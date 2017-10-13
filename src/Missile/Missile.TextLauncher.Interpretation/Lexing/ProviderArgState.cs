namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    ///     State representing the start of collecting arguments for the provider
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.PrimaryArgState" />
    internal class ProviderArgState : PrimaryArgState
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Missile.TextLauncher.Interpretation.Lexing.ProviderArgState" />
        ///     class.
        /// </summary>
        /// <param name="identifier">The identifier</param>
        public ProviderArgState(string identifier) : base(identifier)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the token for this provider
        /// </summary>
        /// <returns>Token for this provider</returns>
        public override Token GetToken()
        {
            return new ProviderToken(Identifier, Args.ToArray());
        }
    }
}