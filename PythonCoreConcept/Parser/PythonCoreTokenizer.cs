
// PythonCore - Tokenizer for Python 3.9 grammar.
// Written by Richard Magnor Stenbro. (C) 2021 By Richard Magnor Stenbro
// Free to use for none commercial uses.

using System;
using System.Collections.Generic;

namespace PythonCoreConcept.Parser
{
    public class PythonCoreTokenizer
    {
        
        private Dictionary<string, TokenKind> _reservedKeywordTable = new Dictionary<string, TokenKind>()
        {
            { "False", TokenKind.PyFalse },
            { "None", TokenKind.PyNone },
            { "True", TokenKind.PyTrue },
            { "and", TokenKind.PyAnd },
            { "as", TokenKind.PyAs },
            { "assert", TokenKind.PyAssert },
            { "async", TokenKind.PyAsync },
            { "await", TokenKind.PyAwait },
            { "break", TokenKind.PyBreak },
            { "class", TokenKind.PyClass },
            { "continue", TokenKind.PyContinue },
            { "def", TokenKind.PyDef },
            { "del", TokenKind.PyDel },
            { "elif", TokenKind.PyElif },
            { "else", TokenKind.PyElse },
            { "except", TokenKind.PyExcept },
            { "finally", TokenKind.PyFinally },
            { "for", TokenKind.PyFor },
            { "from", TokenKind.PyFrom },
            { "global", TokenKind.PyGlobal },
            { "if", TokenKind.PyIf },
            { "import", TokenKind.PyImport },
            { "in", TokenKind.PyIn },
            { "is", TokenKind.PyIs },
            { "lambda", TokenKind.PyLambda },
            { "nonlocal", TokenKind.PyNonLocal },
            { "not", TokenKind.PyNot },
            { "or", TokenKind.PyOr },
            { "pass", TokenKind.PyPass },
            { "raise", TokenKind.PyRaise },
            { "return", TokenKind.PyReturn },
            { "try", TokenKind.PyTry },
            { "while", TokenKind.PyWhile },
            { "with", TokenKind.PyWith },
            { "yield", TokenKind.PyYield }
        };

        private char[] _sourceBuffer;
        private UInt32 _index;

        public Token CurSymbol { get; private set; }
        public UInt32 Position { get; private set; }
        
        
        public PythonCoreTokenizer(char[] sourceCode)
        {
            _sourceBuffer = sourceCode;
            Position = 0;
            _index = 0;
            CurSymbol = null;
        }

        public void Advance()
        {
            
        }
    }
}