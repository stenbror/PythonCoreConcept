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
    
    public record UnaryPlus(UInt32 StartPos, UInt32 EndPos, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record UnaryMinus(UInt32 StartPos, UInt32 EndPos, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record UnaryBitInvert(UInt32 StartPos, UInt32 EndPos, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Mul(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Div(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record FloorDiv(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Modulo(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Matrice(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Plus(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Minus(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record ShiftLeft(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record ShiftRight(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record BitAnd(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record BitXor(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record BitOr(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record StarExpr(UInt32 StartPos, UInt32 EndPos, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompareLess(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompareLessEqual(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompareEqual(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompareGreaterEqual(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompareGreater(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompareNotEqual(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompareIs(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompareIn(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompareIsNot(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol1, Token Symbol2, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompareNotIn(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol1, Token Symbol2, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record NotTest(UInt32 StartPos, UInt32 EndPos, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record AndTest(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record OrTest(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Lambda(UInt32 StartPos, UInt32 EndPos, Token Symbol1, ExpressionNode Left, Token Symbol2, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Test(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol1, ExpressionNode Right, Token Symbol2, ExpressionNode Next) 
        : ExpressionNode(StartPos, EndPos);
    
    public record NamedExpr(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    
    public record StatementNode(UInt32 StartPos, UInt32 EndPos) : Node(StartPos, EndPos);
}
