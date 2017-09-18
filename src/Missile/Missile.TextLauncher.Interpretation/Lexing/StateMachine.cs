using System.Collections.Generic;

namespace Missile.TextLauncher.Interpretation.Lexing
{
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
}