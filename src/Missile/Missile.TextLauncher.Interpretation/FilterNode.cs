namespace Missile.TextLauncher.Interpretation
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