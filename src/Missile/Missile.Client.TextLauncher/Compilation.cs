using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.Client.TextLauncher
{

    public interface ILexer
    {
        IEnumerable<IToken> Lex(string input);
    }

    public class Lexer : ILexer
    {
        public IEnumerable<IToken> Lex(string input)
        {
            throw new NotImplementedException();
        }
    }

    public interface IParser
    {
        AstNode Parse(IEnumerable<AstNode> nodes);
    }

    public class AstNode
    {
        public IToken Token { get; set; }
        public List<AstNode> Children { get; set; }
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
        string[] Args { get; set; }
    }

    public class ProviderToken : IToken
    {
        public string Name { get; set; }
        public string[] Args { get; set; }
    }

    public class FilterToken : IToken
    {
        public string Name { get; set; }
        public string[] Args { get; set; }
    }

    public class DestinationToken : IToken
    {
        public string Name { get; set; }
        public string[] Args { get; set; }
    }
}
