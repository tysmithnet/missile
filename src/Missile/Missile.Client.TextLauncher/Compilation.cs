using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Missile.Client.TextLauncher
{

    public interface ILexer
    {
        IEnumerable<IToken> Lex(string input);
    }

    public interface IParser
    {
        AstNode Parse(IEnumerable<AstNode> nodes);
    }

    public class AstNode
    {
        public IToken Token { get; set; }
        public string ArgString { get; set; }
    }

    public class RootNode : AstNode
    {
        public ProviderNode ProviderNode { get; set; }
        public List<FilterNode> FilterNodes { get; set; }
        public DestinationNode DestinationNode { get; set; }
    }

    public class FilterNode : AstNode
    {

    }

    public class DestinationNode : AstNode
    {

    }

    public class ProviderNode : AstNode
    {
        public string[] Args { get; set; }
    }

    public class Parser : IParser
    {
        public AstNode Parse(IEnumerable<AstNode> nodes)
        {
            throw new NotImplementedException();
        }
    }

    public interface IInterpreter
    {
        void Interpret(AstNode root);
    }

    public class Interpreter : IInterpreter
    {
        public void Interpret(AstNode root)
        {
            throw new NotImplementedException();
        }
    }

    public interface IToken
    {
        string Name { get; set; }
        string ArgString { get; set; }
    }

    public struct ProviderToken : IToken
    {
        public string Name { get; set; }
        public string ArgString { get; set; }
    }

    public struct FilterToken : IToken
    {
        public string Name { get; set; }
        public string ArgString { get; set; }
    }

    public struct DestinationToken : IToken
    {
        public string Name { get; set; }
        public string ArgString { get; set; }
    }
}
