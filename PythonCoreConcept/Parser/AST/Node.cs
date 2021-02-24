using System;

namespace PythonCoreConcept.Parser.AST
{
    public record Node(UInt32 StartPos, UInt32 EndPos );

    public record ExpressionNode(UInt32 StartPos, UInt32 EndPos) : Node(StartPos, EndPos);

    public record AtomFalse(UInt32 StartPos, UInt32 EndPos, Token Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomTrue(UInt32 StartPos, UInt32 EndPos, Token Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomNone(UInt32 StartPos, UInt32 EndPos, Token Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomElipsis(UInt32 StartPos, UInt32 EndPos, Token Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomName(UInt32 StartPos, UInt32 EndPos, Token Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomNumber(UInt32 StartPos, UInt32 EndPos, Token Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomString(UInt32 StartPos, UInt32 EndPos, Token[] Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomTuple(UInt32 StartPos, UInt32 EndPos, Token Symbol1, ExpressionNode Right, Token Symbol2) 
        : ExpressionNode(StartPos, EndPos);
   
    public record AtomList(UInt32 StartPos, UInt32 EndPos, Token Symbol1, ExpressionNode Right, Token Symbol2) 
        : ExpressionNode(StartPos, EndPos);
    
    public record AtomDictionary(UInt32 StartPos, UInt32 EndPos, Token Symbol1, ExpressionNode Right, Token Symbol2) 
        : ExpressionNode(StartPos, EndPos);
    
    public record AtomSet(UInt32 StartPos, UInt32 EndPos, Token Symbol1, ExpressionNode Right, Token Symbol2) 
        : ExpressionNode(StartPos, EndPos);
    
    public record AtomExpr(UInt32 StartPos, UInt32 EndPos, Token Symbol, ExpressionNode Left, ExpressionNode[] Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Power(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record StatementNode(UInt32 StartPos, UInt32 EndPos) : Node(StartPos, EndPos);
}
