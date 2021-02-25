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
    
    public record TestListComp(UInt32 StartPos, UInt32 EndPos, ExpressionNode[] Nodes, Token[] Separators) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Call(UInt32 StartPos, UInt32 EndPos, Token Symbol1, ExpressionNode Right, Token Symbol2) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Index(UInt32 StartPos, UInt32 EndPos, Token Symbol1, ExpressionNode Right, Token Symbol2) 
        : ExpressionNode(StartPos, EndPos);
    
    public record DotName(UInt32 StartPos, UInt32 EndPos, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record SubscriptList(UInt32 StartPos, UInt32 EndPos, ExpressionNode[] Nodes, Token[] Separators) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Subscript(UInt32 StartPos, UInt32 EndPos,
            ExpressionNode First, Token Symbol1, ExpressionNode Two, Token Symbol2, ExpressionNode Three) 
        : ExpressionNode(StartPos, EndPos);
    
    public record ExprList(UInt32 StartPos, UInt32 EndPos, ExpressionNode[] Nodes, Token[] Separators) 
        : ExpressionNode(StartPos, EndPos);
    
    public record TestList(UInt32 StartPos, UInt32 EndPos, ExpressionNode[] Nodes, Token[] Separators) 
        : ExpressionNode(StartPos, EndPos);
    
    public record DictionaryContainer(UInt32 StartPos, UInt32 EndPos,
            ExpressionNode[] Entries, Token[] Separators,
            Token PowerSymbol, ExpressionNode PowerNode) 
        : ExpressionNode(StartPos, EndPos);
    
    public record DictionaryEntry(UInt32 StartPos, UInt32 EndPos, 
            ExpressionNode Key, Token Symbol, ExpressionNode Value) 
        : ExpressionNode(StartPos, EndPos);
    
    public record SetContainer(UInt32 StartPos, UInt32 EndPos, 
            ExpressionNode[] Nodes, Token[] Separators,
            Token MulSymbol, ExpressionNode MulNode) 
        : ExpressionNode(StartPos, EndPos);
    
    public record ArgList(UInt32 StartPos, UInt32 EndPos, ExpressionNode[] Nodes, Token[] Separators) 
        : ExpressionNode(StartPos, EndPos);
    
    public record Argument(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record SyncCompFor(UInt32 StartPos, UInt32 EndPos, 
            Token Symbol1, ExpressionNode Left, Token Symbol2, ExpressionNode Right, ExpressionNode Next) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompFor(UInt32 StartPos, UInt32 EndPos, Token Symbol1, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record CompIf(UInt32 StartPos, UInt32 EndPos, Token Symbol1, ExpressionNode Right, ExpressionNode Next) 
        : ExpressionNode(StartPos, EndPos);
    
    public record YieldFrom(UInt32 StartPos, UInt32 EndPos, Token Symbol1, Token Symbol2, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record YieldExpr(UInt32 StartPos, UInt32 EndPos, Token Symbol1, Node Right) 
        : ExpressionNode(StartPos, EndPos);
    
    public record StatementNode(UInt32 StartPos, UInt32 EndPos) : Node(StartPos, EndPos);
    
    public record IfStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, ExpressionNode Left, Token Symbol2, ExpressionNode Right,
            StatementNode[] Nodes, StatementNode Next) 
        : Node(StartPos, EndPos);
    
    public record ElifStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, ExpressionNode Left, Token Symbol2, ExpressionNode Right) 
        : Node(StartPos, EndPos);
    
    public record ElseStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol1, Token Symbol2, StatementNode Right) 
        : Node(StartPos, EndPos);
    
    public record WhileStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, ExpressionNode Left, Token Symbol2, StatementNode Right, StatementNode Next) 
        : Node(StartPos, EndPos);
    
    public record ForStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, ExpressionNode Left, Token Symbol2, ExpressionNode Right, Token Symbol3, Token Symbol4,
            StatementNode Next, StatementNode Extra) 
        : Node(StartPos, EndPos);
    
    public record TryStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, Token Symbol2, StatementNode Left,
            StatementNode[] ExceptNodes, StatementNode ElseNode, 
            Token Symbol3, Token Symbol4, StatementNode Right) 
        : Node(StartPos, EndPos);
    
    public record ExceptStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, ExpressionNode Left, Token Symbol2, Token Symbol3) 
        : Node(StartPos, EndPos);
    
    public record WithStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, StatementNode[] WithItems, Token[] Separators, Token Symbol2, Token Symbol3,
            StatementNode Right) 
        : Node(StartPos, EndPos);
    
    public record SuiteStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, Token Symbol2, StatementNode[] Nodes, Token Symbol3) 
        : Node(StartPos, EndPos);
}
