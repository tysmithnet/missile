using System.Threading.Tasks;
using Missile.TextLauncher.Interpretation.Parsing;

namespace Missile.TextLauncher.Interpretation
{
    public interface IInterpreter
    {
        Task Interpret(RootNode rootNode);
    }
}