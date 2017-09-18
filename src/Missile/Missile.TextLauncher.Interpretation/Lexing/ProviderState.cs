using Missile.TextLauncher.Interpretation.Parsing;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class ProviderState : PrimaryState
    {
        public ProviderState(string identifier)
        {
            Identifier = identifier;
        }


        public override Token GetToken()
        {
            return new ProviderToken(Identifier, new string[0]);
        }

        public override PrimaryArgState GetArgState()
        {
            return new ProviderArgState(Identifier);
        }
    }
}