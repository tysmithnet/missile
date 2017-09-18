namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class StartState : State
    {
        public override State Transition(char input)
        {
            if (input == ' ')
                return this;
            if (IdentifierRegex.IsMatch(input.ToString()))
                return new ProviderState(input.ToString());

            return null;
        }
    }
}