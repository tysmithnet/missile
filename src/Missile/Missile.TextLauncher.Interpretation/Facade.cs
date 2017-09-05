﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Missile.TextLauncher.Interpretation
{
    [Export(typeof(IFacade))]
    public class Facade : IFacade
    {
        [Import]
        public ILexer Lexer { get; set; }

        [Import]
        public IParser Parser { get; set; }

        [Import]
        public IInterpreter Interpreter { get; set; }
        
        public Task Execute(string input)
        {
            var tokens = Lexer.Lex(input);
            var rootNode = Parser.Parse(tokens);
            return Interpreter.Interpret(rootNode);
        }
    }
}
