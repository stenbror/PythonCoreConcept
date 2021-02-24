using System;

namespace PythonCoreConcept.Parser.AST
{
    public record Node(UInt32 StartPos, UInt32 EndPos );

    public record ExpressionNode(UInt32 StartPos, UInt32 EndPos) : Node(StartPos, EndPos);

    public record AtomFalse(UInt32 StartPos, UInt32 EndPos, Token Symbol) : ExpressionNode(StartPos, EndPos);
    
    
    
    public record StatementNode(UInt32 StartPos, UInt32 EndPos) : Node(StartPos, EndPos);
}