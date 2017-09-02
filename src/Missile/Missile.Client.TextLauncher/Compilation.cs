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
        IEnumerable<Token> Lex(string input);
    }

    public interface IParser
    {
        AstNode Parse(IEnumerable<AstNode> nodes);
    }

    public class AstNode
    {
        public Token Token { get; set; }
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

    public abstract class Token
    {
        public string Identifier { get; protected internal set; }
        public string ArgString { get; protected internal set; }

        internal Token()
        {
            
        }

        protected Token(string input)
        {
            input = input.TrimStart();
            int i;
            for (i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    break;
                }
            }
            Identifier = input.Substring(0, i);
            ArgString = input.Substring(Math.Min(input.Length, i + 1));
        }

        protected Token(string id, string args)
        {
            Identifier = id;
            ArgString = args;
        }

        public override bool Equals(object obj)
        {
            var token = obj as Token;
            return token != null &&
                   Identifier == token.Identifier &&
                   ArgString == token.ArgString;
        }

        public override int GetHashCode()
        {
            var hashCode = -672673088;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Identifier);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ArgString);
            return hashCode;
        }

        public static bool operator ==(Token token1, Token token2)
        {
            return EqualityComparer<Token>.Default.Equals(token1, token2);
        }

        public static bool operator !=(Token token1, Token token2)
        {
            return !(token1 == token2);
        }
    }

    public class ProviderToken : Token
    {
        internal ProviderToken()
        {
            
        }
        
        public ProviderToken(string input) : base(input)
        {

        }
    }

    public class OperatorToken : Token
    {
        internal OperatorToken()
        {
            
        }

        public OperatorToken(string id) : base(id, "")
        {
            
        }
    }

    public class FilterToken : Token
    {
        internal FilterToken()
        {
            
        }

        public  FilterToken(string input) : base(input)
        {

        }
    }

    public class DestinationToken : Token
    {
        public DestinationToken(string input) : base(input)
        {
            
        }
    }
}
