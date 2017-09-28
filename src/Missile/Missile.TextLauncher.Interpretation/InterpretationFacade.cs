﻿using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Missile.TextLauncher.Interpretation.Lexing;
using Missile.TextLauncher.Interpretation.Parsing;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(IInterpretationFacade))]
    public class InterpretationFacade : IInterpretationFacade
    {
        [Import]
        protected internal ILexer Lexer { get; set; }

        [Import]
        protected internal IParser Parser { get; set; }

        [Import]
        protected internal IInterpreter Interpreter { get; set; }

        public Task ExecuteAsync(string input)
        {
            var tokens = Lexer.Lex(input);
            var rootNode = Parser.Parse(tokens);
            return Interpreter.Interpret(rootNode);
        }
    }
}