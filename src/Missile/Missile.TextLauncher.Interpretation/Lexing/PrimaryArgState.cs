using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    /// <inheritdoc />
    /// <summary>
    ///     Providers, filters, and destinations can all take arguments
    ///     This is the state representing the start of collecting those arguments
    /// </summary>
    /// <seealso cref="T:Missile.TextLauncher.Interpretation.Lexing.State" />
    internal abstract class PrimaryArgState : State
    {
        /// <summary>
        ///     The is escaped flag is set when the start of an escaped character is found
        /// </summary>
        private bool _isEscaped;

        /// <summary>
        ///     The is open quote flag is set when an opening quote has been found
        /// </summary>
        private bool _isOpenQuote;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PrimaryArgState" /> class
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <exception cref="ArgumentNullException">identifier</exception>
        protected internal PrimaryArgState(string identifier)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }

        /// <summary>
        ///     Gets or sets the identifier for the current provider, filter, destination
        /// </summary>
        /// <value>
        ///     The identifier
        /// </value>
        public string Identifier { get; set; }

        /// <summary>
        ///     Gets or sets the current argument
        /// </summary>
        /// <value>
        ///     The current argument
        /// </value>
        public string CurrentArg { get; set; } = "";


        /// <summary>
        ///     Gets or sets the arguments
        /// </summary>
        /// <value>
        ///     The arguments
        /// </value>
        public List<string> Args { get; } = new List<string>();

        /// <inheritdoc />
        /// <summary>
        ///     Transitions this state to the next asynchronously
        /// </summary>
        /// <param name="input">The input to lex</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>
        ///     A task that when complete will have the next state in hand
        /// </returns>
        public override async Task<State> TransitionAsync(char input, CancellationToken cancellationToken)
        {
            switch (input)
            {
                case '\\':
                    if (_isEscaped)
                    {
                        _isEscaped = false;
                        CurrentArg += '\\';
                    }
                    else
                    {
                        _isEscaped = true;
                    }
                    return this;
                case ' ':
                    if (_isOpenQuote)
                    {
                        CurrentArg += input;
                        return this;
                    }
                    FlushCurrentArg();
                    return this;
                case '|':
                    FlushCurrentArg();
                    OnRaiseTokenEvent(new TokenEventArgs(GetToken()));
                    OnRaiseTokenEvent(new TokenEventArgs(new OperatorToken("|", new string[0])));
                    return new FilterState();
                case '"':
                    if (_isEscaped)
                    {
                        CurrentArg += "\"";
                        _isEscaped = false;
                        return this;
                    }

                    if (_isOpenQuote)
                    {
                        Args.Add(CurrentArg);
                        CurrentArg = "";
                    }
                    else
                    {
                        _isOpenQuote = true;
                    }
                    return this;
                case '>':
                    FlushCurrentArg();
                    OnRaiseTokenEvent(new TokenEventArgs(GetToken()));
                    OnRaiseTokenEvent(new TokenEventArgs(new OperatorToken(">", new string[0])));
                    return new DestinationState();
                default:
                    if (_isEscaped)
                    {
                        char x;
                        switch (input)
                        {
                            case 't':
                                x = '\t';
                                break;
                            case 'n':
                                x = '\n';
                                break;
                            case 'r':
                                x = '\r';
                                break;
                            // todo: quotes, slashes, 
                            default:
                                throw new FormatException($"Unrecognized escape sequence: \\{input}");
                        }
                        CurrentArg += x;
                        _isEscaped = false;
                    }
                    else
                    {
                        CurrentArg += input;
                    }
                    break;
            }

            return this;
        }

        private void FlushCurrentArg()
        {
            if (!string.IsNullOrWhiteSpace(CurrentArg))
                Args.Add(CurrentArg);
            CurrentArg = "";
        }

        /// <summary>
        ///     Gets the token for this provider, filter, desintation
        /// </summary>
        /// <returns></returns>
        public abstract Token GetToken();

        /// <inheritdoc />
        /// <summary>
        ///     Returns the provider, filter, destination token in progress
        /// </summary>
        public override void Flush()
        {
            if (!string.IsNullOrEmpty(CurrentArg))
            {
                Args.Add(CurrentArg);
                CurrentArg = "";
            }

            if (!string.IsNullOrWhiteSpace(Identifier))
                OnRaiseTokenEvent(new TokenEventArgs(GetToken()));
        }
    }
}