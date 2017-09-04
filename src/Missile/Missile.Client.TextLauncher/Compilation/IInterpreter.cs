using System.Threading.Tasks;

namespace Missile.Client.TextLauncher.Compilation
{
    public interface IInterpreter
    {
        Task Interpret(RootNode root);
    }
}
