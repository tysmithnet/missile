using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Missile.TextLauncher.Interpretation.Lexing;
using Missile.TextLauncher.Interpretation.Parsing;
using Moq;
using Xunit;

namespace Missile.TextLauncher.Interpretation.Tests
{
    public class InterpretationFacade_Should
    {
        [Fact]
        public void Cancel_If_Requested_Before_Interpreting()
        {
            var mockLexer = new Mock<ILexer>();
            mockLexer.Setup(lexer => lexer.LexAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Token[0], TimeSpan.FromMilliseconds(1));

            var mockParser = new Mock<IParser>();
            mockParser.Setup(parser => parser.ParseAsync(It.IsAny<IEnumerable<Token>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RootNode(), TimeSpan.FromMilliseconds(500));

            var mockInterpreter = new Mock<IInterpreter>();
            mockInterpreter
                .Setup(interpreter => interpreter.InterpretAsync(It.IsAny<RootNode>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var facade = new InterpretationFacade
            {
                Lexer = mockLexer.Object,
                Parser = mockParser.Object,
                Interpreter = mockInterpreter.Object
            };
            var cts = new CancellationTokenSource(200);
            Func<Task> f = async () => await facade.ExecuteAsync("this doesnt matter", cts.Token);
            f.ShouldThrow<AggregateException>("because cancel was called before interpreter started");
        }

        [Fact]
        public void Cancel_If_Requested_Before_Parsing()
        {
            var mockLexer = new Mock<ILexer>();
            mockLexer.Setup(lexer => lexer.LexAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Token[0], TimeSpan.FromMilliseconds(500));

            var mockParser = new Mock<IParser>();
            mockParser.Setup(parser => parser.ParseAsync(It.IsAny<IEnumerable<Token>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RootNode());

            var mockInterpreter = new Mock<IInterpreter>();
            mockInterpreter
                .Setup(interpreter => interpreter.InterpretAsync(It.IsAny<RootNode>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var facade = new InterpretationFacade
            {
                Lexer = mockLexer.Object,
                Parser = mockParser.Object,
                Interpreter = mockInterpreter.Object
            };
            var cts = new CancellationTokenSource(200);
            Func<Task> f = async () => await facade.ExecuteAsync("this doesnt matter", cts.Token);
            f.ShouldThrow<AggregateException>("because cancel was called before parser started");
        }

        [Fact]
        public void Not_Throw_If_No_Problems()
        {
            var mockLexer = new Mock<ILexer>();
            mockLexer.Setup(lexer => lexer.LexAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Token[0], TimeSpan.FromMilliseconds(1));

            var mockParser = new Mock<IParser>();
            mockParser.Setup(parser => parser.ParseAsync(It.IsAny<IEnumerable<Token>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RootNode(), TimeSpan.FromMilliseconds(1));

            var mockInterpreter = new Mock<IInterpreter>();
            mockInterpreter
                .Setup(interpreter => interpreter.InterpretAsync(It.IsAny<RootNode>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var facade = new InterpretationFacade
            {
                Lexer = mockLexer.Object,
                Parser = mockParser.Object,
                Interpreter = mockInterpreter.Object
            };
            var cts = new CancellationTokenSource();
            Func<Task> f = async () => await facade.ExecuteAsync("this doesnt matter", cts.Token);
            f.ShouldNotThrow("because each component executed with out error or being cancelled");
        }
    }
}