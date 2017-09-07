using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    public interface IInterpreter
    {
        Task Interpret(RootNode rootNode);
    }
}