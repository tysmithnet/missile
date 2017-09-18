namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class FilterState : PrimaryState
    {
        public override Token GetToken()
        {
            return new FilterToken(Identifier, new string[0]);
        }

        public override PrimaryArgState GetArgState()
        {
            return new FilterArgState(Identifier);
        }
    }
}