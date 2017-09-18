using System;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class ErrorState : State
    {
        public override State Transition(char input)
        {
            throw new NotImplementedException();
        }
    }
}