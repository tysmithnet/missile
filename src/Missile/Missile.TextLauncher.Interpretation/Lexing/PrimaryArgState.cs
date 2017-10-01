using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal abstract class PrimaryArgState : State
    {
        private bool isEscaped;

        private bool isOpenQuote;

        protected PrimaryArgState(string identifier)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }

        public string Identifier { get; set; }
        public string CurrentArg { get; set; } = "";
        public List<string> Args { get; set; } = new List<string>();

        public override async Task<State> TransitionAsync(char input, CancellationToken cancellationToken)
        {
            if (input == '\\')
            {
                if (isEscaped)
                {
                    isEscaped = false;
                    CurrentArg += '\\';
                }
                else
                {
                    isEscaped = true;
                }
                return this;
            }

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
                if (isEscaped)
                {
                    CurrentArg += "\"";
                    isEscaped = false;
                    return this;
                }

                if (isOpenQuote)
                {
                    Args.Add(CurrentArg);
                    CurrentArg = "";
                }
                else
                {
                    isOpenQuote = true;
                }
                return this;
            }

            if (input == '>')
            {
                OnRaiseTokenEvent(new TokenEventArgs(GetToken()));
                OnRaiseTokenEvent(new TokenEventArgs(new OperatorToken(">", new string[0])));
                return new DestinationState();
            }

            CurrentArg += input;
            return this;
        }

        public abstract Token GetToken();

        public override void Flush()
        {
            if (!string.IsNullOrWhiteSpace(CurrentArg))
            {
                Args.Add(CurrentArg);
                CurrentArg = "";
            }

            if (!string.IsNullOrWhiteSpace(Identifier))
                OnRaiseTokenEvent(new TokenEventArgs(GetToken()));
        }
    }
}