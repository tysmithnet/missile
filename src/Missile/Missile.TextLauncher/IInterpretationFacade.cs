using System.Threading.Tasks;

namespace Missile.TextLauncher
{
    public interface IInterpretationFacade
    {    
        Task Execute(string input);
    }
}