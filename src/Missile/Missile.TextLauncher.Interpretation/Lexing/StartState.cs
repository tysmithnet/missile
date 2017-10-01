using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class StartState : State
    {
       
        public override async Task<State> TransitionAsync(char input, CancellationToken cancellationToken)
        {
            if (input == ' ')
                return this;
            if (IdentifierRegex.IsMatch(input.ToString()))
                return new ProviderState(input.ToString());

            return null;
        }
    }
}