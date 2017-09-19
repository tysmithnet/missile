namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal abstract class PrimaryState : State
    {
        public string Identifier { get; set; }

        public sealed override State Transition(char input)
        {
            if (input == ' ')
            {
                if (Identifier == null)
                    return this;
                return GetArgState();
            }

            if (IdentifierRegex.IsMatch(input.ToString()))
            {
                Identifier += input;
                return this;
            }

            return new ErrorState();
        }

        public override void Flush()
        {
            if (!string.IsNullOrWhiteSpace(Identifier))
                OnRaiseTokenEvent(new TokenEventArgs(GetToken()));
        }

        public abstract Token GetToken();

        public abstract PrimaryArgState GetArgState();
    }
}