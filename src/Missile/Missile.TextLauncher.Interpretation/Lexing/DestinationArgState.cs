namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class DestinationArgState : PrimaryArgState
    {
        public DestinationArgState(string identifier) : base(identifier)
        {
        }

        public override Token GetToken()
        {
            if (!string.IsNullOrWhiteSpace(CurrentArg))
            {
                Args.Add(CurrentArg);
                CurrentArg = "";
            }

            return new DestinationToken(Identifier, Args.ToArray());
        }
    }
}