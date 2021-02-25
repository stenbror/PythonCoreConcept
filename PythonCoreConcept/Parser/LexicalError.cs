using System;

namespace PythonCoreConcept.Parser
{
    public class LexicalError : Exception
    {
        public LexicalError(UInt32 position, string msg) : base() {}
    }
}