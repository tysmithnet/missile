using System.Threading;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Lexing
{
    internal class DestinationState : PrimaryState
    {
        public override Token GetToken()
        {
            return new DestinationToken(Identifier, new string[0]);
        }

        public override PrimaryArgState GetArgState()
        {
            return new DestinationArgState(Identifier);
        }
    }
}