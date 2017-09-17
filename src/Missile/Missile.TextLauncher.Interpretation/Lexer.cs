using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(ILexer))]
    public class Lexer : ILexer
    {
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
                if (Regex.IsMatch(input.ToString(), "[a-zA-Z0-9_]"))
                    return new ProviderState(input.ToString());

                return null;
            }
        }

        internal class ProviderState : State
        {
            public ProviderState(string providerName)
            {
                ProviderName = providerName;
            }

            public string ProviderName { get; set; }

            public override State Transition(char input)
            {
                if (input == ' ')
                    return new ProviderArgsState(ProviderName);

                if (Regex.IsMatch(input.ToString(), "[a-zA-Z0-9_]"))
                {
                    ProviderName += input;
                    return this;
                }

                return new ErrorState();
            }

            public override void Flush()
            {
                OnRaiseTokenEvent(new TokenEventArgs(new ProviderToken(ProviderName, new string[0])));
            }
        }

        internal class ProviderArgsState : State
        {
            private readonly List<string> args = new List<string>();
            private string currentArg = "";

            public ProviderArgsState(string providerName)
            {
                ProviderName = providerName;
            }

            public string ProviderName { get; set; }

            public override State Transition(char input)
            {
                if (input == ' ')
                {
                    args.Add(currentArg);
                    currentArg = "";
                    return this;
                }
                currentArg += input;
                return this;
            }

            public override void Flush()
            {
                if (!string.IsNullOrWhiteSpace(currentArg))
                    args.Add(currentArg);
                OnRaiseTokenEvent(new TokenEventArgs(new ProviderToken(ProviderName, args.ToArray())));
            }
        }

        internal class FilterState : State
        {
            public string FilterName { get; set; }

            public override State Transition(char input)
            {
                throw new NotImplementedException();
            }
        }

        internal class DestinationState
        {
            public string DestinationName { get; set; }
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