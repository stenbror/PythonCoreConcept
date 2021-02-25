using System;

namespace PythonCoreConcept.Parser
{
    public class SyntaxError : Exception
    {
        public SyntaxError(UInt32 position, string msg, Token token) : base() {}
    }
}