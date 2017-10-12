namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    /// Token for provider, filter, or destination
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.Token" />
    public class ProviderToken : Token
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Missile.TextLauncher.Interpretation.Lexing.ProviderToken" /> class.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <param name="args">The arguments.</param>
        public ProviderToken(string providerName, string[] args) : base(providerName, args)
        {
        }
    }
}