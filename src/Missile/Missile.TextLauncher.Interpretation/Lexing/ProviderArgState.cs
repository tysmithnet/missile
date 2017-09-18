using Missile.TextLauncher.Interpretation.Parsing;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class ProviderArgState : PrimaryArgState
    {
        public ProviderArgState(string identifier) : base(identifier)
        {
        }

        public override Token GetToken()
        {
            if (!string.IsNullOrWhiteSpace(CurrentArg))
            {
                Args.Add(CurrentArg);
                CurrentArg = "";
            }

            return new ProviderToken(Identifier, Args.ToArray());
        }
    }
}