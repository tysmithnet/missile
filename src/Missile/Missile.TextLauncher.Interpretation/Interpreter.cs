using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(IInterpreter))]
    public class Interpreter : IInterpreter
    {
        public Task Interpret(RootNode rootNode)
        {
            return Task.CompletedTask;
        }
    }
}
