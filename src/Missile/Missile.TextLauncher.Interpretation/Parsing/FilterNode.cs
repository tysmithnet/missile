using Missile.TextLauncher.Interpretation.Lexing;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    public class FilterNode : Node
    {
        public FilterNode(FilterToken filterToken)
        {
            Name = filterToken.Name;
            Args = filterToken.Args;
        }
    }
}