using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class StateMachine
    {
        public State CurrentState { get; set; }

        public async Task<IEnumerable<Token>> RunAsync(string input, CancellationToken cancellationToken)
        {
            input = input ?? "";
            var tokens = new List<Token>();
            CurrentState = new StartState();
            State.RaiseTokenEvent += (sender, args) => tokens.Add(args.Token);
            for (var i = 0; i < input.Length; i++)
                CurrentState = await CurrentState.TransitionAsync(input[i], cancellationToken);
            CurrentState.Flush();
            return tokens;
        }
    }
}