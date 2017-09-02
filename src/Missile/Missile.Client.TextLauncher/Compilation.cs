﻿using System;
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
        RootNode Parse(IEnumerable<Token> list);
    }

    public abstract class Node
    {
        public string Name { get; set; }
        public string ArgString { get; set; }
    }

    public class RootNode
    {
        public ProviderNode ProviderNode { get; set; }
        public List<FilterNode> FilterNodes { get; set; }
        public DestinationNode DestinationNode { get; set; }

        public override bool Equals(object obj)
        {
            var node = obj as RootNode;
            return node != null &&
                   base.Equals(obj) &&
                   EqualityComparer<ProviderNode>.Default.Equals(ProviderNode, node.ProviderNode) &&
                   EqualityComparer<List<FilterNode>>.Default.Equals(FilterNodes, node.FilterNodes) &&
                   EqualityComparer<DestinationNode>.Default.Equals(DestinationNode, node.DestinationNode);
        }

        public override int GetHashCode()
        {
            var hashCode = -74872637;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ProviderNode>.Default.GetHashCode(ProviderNode);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<FilterNode>>.Default.GetHashCode(FilterNodes);
            hashCode = hashCode * -1521134295 + EqualityComparer<DestinationNode>.Default.GetHashCode(DestinationNode);
            return hashCode;
        }

        public static bool operator ==(RootNode node1, RootNode node2)
        {
            return EqualityComparer<RootNode>.Default.Equals(node1, node2);
        }

        public static bool operator !=(RootNode node1, RootNode node2)
        {
            return !(node1 == node2);
        }
    }

    public class ProviderNode : Node
    {                           
        public ProviderNode(ProviderToken providerToken)
        {
            Name = providerToken.Identifier;
            ArgString = providerToken.ArgString;
        }       
    }

    public class FilterNode : Node
    {          
        public FilterNode(FilterToken filterToken)
        {
            Name = filterToken.Identifier;
            ArgString = filterToken.ArgString;
        }                                   
    }

    public class DestinationNode : Node
    {                                        
        public DestinationNode(DestinationToken destinationToken)
        {
            Name = destinationToken.Identifier;
            ArgString = destinationToken.ArgString;
        }
    }

    public interface IInterpreter
    {
        void Interpret(RootNode root);
    }

    public class Interpreter : IInterpreter
    {
        public void Interpret(RootNode root)
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

        public FilterToken(string input) : base(input)
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
