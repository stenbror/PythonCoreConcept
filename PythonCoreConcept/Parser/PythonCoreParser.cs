
// PythonCore - This is the Recursive descend parser for Python 3.9 grammar.
// Written by Richard Magnor Stenbro. (C) 2021 By Richard Magnor Stenbro
// Free to use for none commercial uses.

using System;

namespace PythonCoreConcept.Parser
{
    public class PythonCoreParser
    {
        private PythonCoreTokenizer Tokenizer;
        private UInt16 TabSize;

        public PythonCoreParser(PythonCoreTokenizer tokenizer, UInt16 tabSize)
        {
            Tokenizer = tokenizer;
            TabSize = tabSize;
        }
    }
}