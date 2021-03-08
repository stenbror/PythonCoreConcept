﻿using System;
using PythonCoreConcept.Parser;
using PythonCoreConcept.Parser.AST;

namespace PythonCoreConcept
{
    public class PythonFrontEnd
    {
        public static Node ParseEvalInput(string sourceCode, UInt32 tabSize = 8u)
        {
            var lex = new PythonCoreTokenizer(sourceCode.ToCharArray());
            lex.TabSize = tabSize;
            var parser = new PythonCoreParser(lex);
            return parser.ParseEvalInput();
        }
    }
}