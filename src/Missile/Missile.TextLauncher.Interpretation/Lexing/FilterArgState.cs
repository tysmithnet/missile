namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class FilterArgState : PrimaryArgState
    {
        public FilterArgState(string identifier) : base(identifier)
        {
        }

        public override Token GetToken()
        {
            return new FilterToken(Identifier, Args.ToArray());
        }
    }
}