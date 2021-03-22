using System;
using System.Diagnostics.SymbolStore;

namespace PythonCoreConcept.Parser
{
    public class SyntaxError : Exception
    {
        public UInt32 Position { get; init; }
        public Token Symbol { get; init; }

        public SyntaxError(UInt32 position, string msg, Token token) : base(msg)
        {
            Position = position;
            Symbol = token;
        }
    }
}