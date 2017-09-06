using System;

namespace Missile.TextLauncher.Interpretation
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