using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(ILexer))]
    public class Lexer : ILexer
    {   
        public IEnumerable<Token> Lex(string input)
        {
            StateMachine stateMachine = new StateMachine();
            return stateMachine.Run(input);                                                
        }  

        internal class StateMachine
        {
            public State CurrentState { get; set; }
            public List<State> PreviousStates = new List<State>();

            public IEnumerable<Token> Run(string input)
            {
                input = input ?? "";
                List<Token> tokens = new List<Token>();
                CurrentState = new StartState();
                for (int i = 0; i < input.Length; i++)
                {   
                    PreviousStates.Add(CurrentState);
                    CurrentState = CurrentState.Transition(input[i]);
                    CurrentState.RaiseTokenEvent += (sender, args) => tokens.Add(args.Token);
                }
                ProviderState providerState = CurrentState as ProviderState;
                if(providerState is ProviderState && !tokens.Any())
                    tokens.Add(new ProviderToken(providerState.ProviderName, new string[0]));
                    
                return tokens;
            }
        }

        internal abstract class State
        {
            public abstract State Transition(char input);

            public event EventHandler<TokenEventArgs> RaiseTokenEvent;

            protected virtual void OnRaiseTokenEvent(TokenEventArgs e)
            {
                RaiseTokenEvent?.Invoke(this, e);
            }
        }

        internal class TokenEventArgs : EventArgs
        {
            public Token Token { get; set; }

            public TokenEventArgs(Token token)
            {
                Token = token ?? throw new ArgumentNullException(nameof(token));
            }
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
            public string ProviderName { get; set; }

            public ProviderState(string providerName)
            {
                ProviderName = providerName;
            }

            public override State Transition(char input)
            {
                if (input == ' ')
                {   
                    ProviderToken providerToken = new ProviderToken(ProviderName, new string[0]);
                    OnRaiseTokenEvent(new TokenEventArgs(providerToken));
                    return new ProviderCompleteState(ProviderName);
                }
                                                               
                if (Regex.IsMatch(input.ToString(), "[a-zA-Z0-9_]"))
                    return new ProviderState(ProviderName + input);

                return new ErrorState();
            }
        }

        internal class ProviderCompleteState : State
        {
            public string ProviderName { get; set; }

            public ProviderCompleteState(string providerName)
            {
                ProviderName = providerName;
            }

            public override State Transition(char input)
            {
                throw new NotImplementedException();
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