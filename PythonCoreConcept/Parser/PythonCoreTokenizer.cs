
// PythonCore - Tokenizer for Python 3.9 grammar.
// Written by Richard Magnor Stenbro. (C) 2021 By Richard Magnor Stenbro
// Free to use for none commercial uses.

using System;

namespace PythonCoreConcept.Parser
{
    public class PythonCoreTokenizer
    {
        private string SourceBuffer;

        public PythonCoreTokenizer(string sourceCode)
        {
            SourceBuffer = sourceCode;
        }
    }
}