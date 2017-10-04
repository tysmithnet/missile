using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Missile.TextLauncher.Interpretation.Lexing;
using Missile.TextLauncher.Interpretation.Parsing;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class InterpretationFacade_Should
    {
        [Fact]
        public void Cancel_If_Requested()
        {
            InterpretationFacade facade = new InterpretationFacade();
            facade.Lexer = new LongRunningLexer();
            facade.Parser = new Parser();
            facade.Interpreter = new InterpreterBuilder().Build();
            var cts = new CancellationTokenSource(200);
            Func<Task> f = async () => await facade.ExecuteAsync("this doesnt matter", cts.Token);
            f.ShouldThrow<AggregateException>("because cancel was called before lexer finished");
        }
    }

    public class LongRunningLexer : ILexer
    {
        public async Task<IEnumerable<Token>> LexAsync(string input, CancellationToken cancellationToken)
        {
            await Task.Delay(500);
            return await new Lexer().LexAsync(input, cancellationToken);
        }
    }
}