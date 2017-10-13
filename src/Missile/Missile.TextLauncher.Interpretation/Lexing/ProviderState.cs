namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    ///     State representing the start of creating a provider
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.PrimaryState" />
    internal class ProviderState : PrimaryState
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ProviderState" /> class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        public ProviderState(string identifier)
        {
            Identifier = identifier;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the token for this provider
        /// </summary>
        /// <returns>
        ///     Get the token for this provider
        /// </returns>
        public override Token GetToken()
        {
            return new ProviderToken(Identifier, new string[0]);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the state of argument collection
        /// </summary>
        /// <returns>
        ///     The state of argument collection
        /// </returns>
        public override PrimaryArgState GetArgState()
        {
            return new ProviderArgState(Identifier);
        }
    }
}