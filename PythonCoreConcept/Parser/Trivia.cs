using System;

namespace PythonCoreConcept.Parser
{
    public record Trivia(UInt32 StartPos, UInt32 EndPos, TriviaKind Kind, String Value);
}