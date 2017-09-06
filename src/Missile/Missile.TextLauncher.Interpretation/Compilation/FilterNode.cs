namespace Missile.TextLauncher.Interpretation.Compilation
{
    public class FilterNode : Node
    {
        public FilterNode(FilterToken filterToken)
        {
            Name = filterToken.Identifier;
            ArgString = filterToken.ArgString;
        }
    }
}