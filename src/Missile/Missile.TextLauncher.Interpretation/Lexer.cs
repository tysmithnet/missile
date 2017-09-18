using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(ILexer))]
    public class Lexer : ILexer
    {
        public static readonly Regex IdentifierRegex = new Regex("[a-zA-Z0-9_]");

        public IEnumerable<Token> Lex(string input)
        {
            var stateMachine = new StateMachine();
            return stateMachine.Run(input);
        }

        internal class StateMachine
        {
            public State CurrentState { get; set; }

            public IEnumerable<Token> Run(string input)
            {
                input = input ?? "";
                var tokens = new List<Token>();
                CurrentState = new StartState();
                State.RaiseTokenEvent += (sender, args) => tokens.Add(args.Token);
                for (var i = 0; i < input.Length; i++)
                    CurrentState = CurrentState.Transition(input[i]);
                CurrentState.Flush();

                return tokens;
            }
        }

        internal abstract class State
        {
            public abstract State Transition(char input);


            public static event EventHandler<TokenEventArgs> RaiseTokenEvent;

            protected virtual void OnRaiseTokenEvent(TokenEventArgs e)
            {
                RaiseTokenEvent?.Invoke(this, e);
            }

            public virtual void Flush()
            {
            }
        }

        internal class TokenEventArgs : EventArgs
        {
            public TokenEventArgs(Token token)
            {
                Token = token ?? throw new ArgumentNullException(nameof(token));
            }

            public Token Token { get; set; }
        }

        internal class StartState : State
        {
            public override State Transition(char input)
            {
                if (input == ' ')
                    return this;
                if (IdentifierRegex.IsMatch(input.ToString()))
                    return new ProviderState(input.ToString());

                return null;
            }
        }

        internal abstract class PrimaryState : State
        {
            public string Identifier { get; set; }

            public sealed override State Transition(char input)
            {
                if (input == ' ')
                {
                    if (Identifier == null)
                        return this;
                    return GetArgState();
                }                        

                if (IdentifierRegex.IsMatch(input.ToString()))
                {
                    Identifier += input;
                    return this;
                }

                return new ErrorState();
            }

            public override void Flush()
            {
                if (!string.IsNullOrWhiteSpace(Identifier))
                    OnRaiseTokenEvent(new TokenEventArgs(GetToken()));
            }

            public abstract Token GetToken();

            public abstract PrimaryArgState GetArgState();
        }

        internal abstract class PrimaryArgState : State
        {
            protected PrimaryArgState(string identifier)
            {
                Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            }

            public string Identifier { get; set; }
            public string CurrentArg { get; set; } = "";
            public List<string> Args { get; set; } = new List<string>();

            private bool isOpenQuote = false;

            public override State Transition(char input)
            {
                if (input == ' ')
                {
                    if (isOpenQuote)
                    {
                        CurrentArg += input;
                        return this;
                    }
                    if (!string.IsNullOrWhiteSpace(CurrentArg))
                        Args.Add(CurrentArg);
                    CurrentArg = "";
                    return this;
                }

                if (input == '|')
                {
                    OnRaiseTokenEvent(new TokenEventArgs(GetToken()));
                    OnRaiseTokenEvent(new TokenEventArgs(new OperatorToken("|", new string[0])));
                    return new FilterState();
                }

                if (input == '"')
                {
                    if (isOpenQuote)
                    {
                        Args.Add(CurrentArg);
                        CurrentArg = "";
                    }
                    else
                        isOpenQuote = true;
                    return this;
                }

                CurrentArg += input;
                return this;
            }

            public abstract Token GetToken();

            public override void Flush()
            {
                if (!string.IsNullOrWhiteSpace(Identifier))
                {
                    if(!string.IsNullOrWhiteSpace(CurrentArg))
                        Args.Add(CurrentArg);
                    OnRaiseTokenEvent(new TokenEventArgs(GetToken()));
                    
                }                                                     
            }
        }

        internal class ProviderState : PrimaryState
        {
            public ProviderState(string identifier)
            {
                Identifier = identifier;
            }


            public override Token GetToken()
            {
                return new ProviderToken(Identifier, new string[0]);
            }

            public override PrimaryArgState GetArgState()
            {
                return new ProviderArgState(Identifier);
            }
        }

        internal class ProviderArgState : PrimaryArgState
        {
            public ProviderArgState(string identifier) : base(identifier)
            {
            }

            public override Token GetToken()
            {
                if (!string.IsNullOrWhiteSpace(CurrentArg))
                    Args.Add(CurrentArg);
                return new ProviderToken(Identifier, Args.ToArray());
            }
        }

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

        internal class FilterArgState : PrimaryArgState
        {
            public FilterArgState(string identifier) : base(identifier)
            {
            }

            public override Token GetToken()
            {
                return new FilterToken(Identifier, Args.ToArray());
            }
        }

        internal class ErrorState : State
        {
            public override State Transition(char input)
            {
                throw new NotImplementedException();
            }
        }
    }
}