using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation.Compilation
{
    public interface IInterpreter
    {
        Task Interpret(RootNode rootNode);
    }
}