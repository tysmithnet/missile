using Missile.TextLauncher.Interpretation.Lexing;

namespace Missile.TextLauncher.Interpretation.Parsing
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents a filter in an abstract syntax tree
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Parsing.Node" />
    public class FilterNode : Node
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FilterNode" /> class.
        /// </summary>
        /// <param name="filterToken">The filter token.</param>
        public FilterNode(FilterToken filterToken)
        {
            Name = filterToken.Name;
            Args = filterToken.Args;
        }
    }
}