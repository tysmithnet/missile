using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public interface IInterpretationFacade
    {
        Task ExecuteAsync(string input);
    }
}