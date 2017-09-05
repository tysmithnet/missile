using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class Interpreter_Should
    {
        [Fact]
        public void Handle_Single_Provider_With_No_Args()
        {                                                   
            Interpreter interpreter = new Interpreter();
            Task task = interpreter.Interpret(null);
            task.Exception.Should().BeNull();
        }
    }
}
