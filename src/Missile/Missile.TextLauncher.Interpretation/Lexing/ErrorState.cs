using System;
using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class ErrorState : State
    {   
        public override Task<State> TransitionAsync(char input, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}