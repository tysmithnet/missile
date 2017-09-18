namespace Missile.TextLauncher.Interpretation.Lexing
{
    public class ProviderToken : Token
    {
        public ProviderToken(string providerName, string[] args) : base(providerName, args)
        {
        }
    }
}