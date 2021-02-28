
// PythonCore - All of Abstract Syntax Tree nodes.
// Written by Richard Magnor Stenbro. (C) 2021 By Richard Magnor Stenbro
// Free to use for none commercial uses.

using System;

namespace PythonCoreConcept.Parser.AST
{
    public record Node(UInt32 StartPos, UInt32 EndPos );

    public record ExpressionNode(UInt32 StartPos, UInt32 EndPos) : Node(StartPos, EndPos);

    public record AtomFalse(UInt32 StartPos, UInt32 EndPos, Token Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomTrue(UInt32 StartPos, UInt32 EndPos, Token Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomNone(UInt32 StartPos, UInt32 EndPos, Token Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomElipsis(UInt32 StartPos, UInt32 EndPos, Token Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomName(UInt32 StartPos, UInt32 EndPos, NameToken Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomNumber(UInt32 StartPos, UInt32 EndPos, NumberToken Symbol) : ExpressionNode(StartPos, EndPos);
    
    public record AtomString(UInt32 StartPos, UInt32 EndPos, StringToken[] Symbol) : ExpressionNode(StartPos, EndPos);
    
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
    
    public record DotName(UInt32 StartPos, UInt32 EndPos, Token Symbol, NameToken Symbol2) 
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
            ExpressionNode[] Entries, Token[] Separators) 
        : ExpressionNode(StartPos, EndPos);
    
    public record DictionaryEntry(UInt32 StartPos, UInt32 EndPos, 
            ExpressionNode Key, Token Symbol, ExpressionNode Value) 
        : ExpressionNode(StartPos, EndPos);

    public record DictionaryKW(UInt32 StartPos, UInt32 EndPos, Token Symbol, ExpressionNode Right)
        : ExpressionNode(StartPos, EndPos);
    
    public record SetContainer(UInt32 StartPos, UInt32 EndPos, 
            ExpressionNode[] Nodes, Token[] Separators) 
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
            Token Symbol1, ExpressionNode Left, Token Symbol2, StatementNode Right,
            StatementNode[] Nodes, StatementNode Next) 
        : StatementNode(StartPos, EndPos);
    
    public record ElifStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, ExpressionNode Left, Token Symbol2, StatementNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record ElseStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol1, Token Symbol2, StatementNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record WhileStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, ExpressionNode Left, Token Symbol2, StatementNode Right, StatementNode Next) 
        : StatementNode(StartPos, EndPos);
    
    public record ForStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, ExpressionNode Left, Token Symbol2, ExpressionNode Right, Token Symbol3, Token Symbol4,
            StatementNode Next, StatementNode Extra) 
        : StatementNode(StartPos, EndPos);
    
    public record TryStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, Token Symbol2, StatementNode Left,
            StatementNode[] ExceptNodes, StatementNode ElseNode, 
            Token Symbol3, Token Symbol4, StatementNode Right) 
        : StatementNode(StartPos, EndPos);

    public record ExceptStatement(UInt32 StartPos, UInt32 EndPos, StatementNode Left, Token Symbol, StatementNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record ExceptClauseStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, ExpressionNode Left, Token Symbol2, NameToken Symbol3) 
        : StatementNode(StartPos, EndPos);
    
    public record WithStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, StatementNode[] WithItems, Token[] Separators, Token Symbol2, Token Symbol3,
            StatementNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record WithItemStatement(UInt32 StartPos, UInt32 EndPos, ExpressionNode Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record SuiteStatement(UInt32 StartPos, UInt32 EndPos,
            Token Symbol1, Token Symbol2, StatementNode[] Nodes, Token Symbol3) 
        : StatementNode(StartPos, EndPos);
    
    public record SimpleStatement(UInt32 StartPos, UInt32 EndPos, StatementNode[] Nodes, Token[] Separators, Token Symbol) 
        : StatementNode(StartPos, EndPos);
    
    public record PlusAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record MinusAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record MulAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record MatriceAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record DivAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record ModuloAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record BitAndAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record BitOrAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record BitXorAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record ShiftLeftAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record ShiftRightAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record PowerAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record FloorDivAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record AnnAssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token Symbol1, ExpressionNode Right, Token Symbol2, Node Next) 
        : StatementNode(StartPos, EndPos);
    
    public record AssignStatement(UInt32 StartPos, UInt32 EndPos, Node Left, Token[] Symbols, Node[] RightNodes, Token Symbol) 
        : StatementNode(StartPos, EndPos);
    
    public record TestListStarExprStatement(UInt32 StartPos, UInt32 EndPos, ExpressionNode[] Nodes, Token[] Separators) 
        : StatementNode(StartPos, EndPos);
    
    public record DelStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol, ExpressionNode Right) : StatementNode(StartPos, EndPos);
    
    public record PassStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol) : StatementNode(StartPos, EndPos);
    
    public record BreakStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol) : StatementNode(StartPos, EndPos);
    
    public record ContinueStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol) : StatementNode(StartPos, EndPos);
    
    public record ReturnStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol, StatementNode Right) : StatementNode(StartPos, EndPos);
    
    public record YieldStatement(UInt32 StartPos, UInt32 EndPos, ExpressionNode Right) : StatementNode(StartPos, EndPos);
    
    public record RaiseStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol1, ExpressionNode Left, Token Symbol2, ExpressionNode Right) : StatementNode(StartPos, EndPos);
    
    public record ImportNameStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol, StatementNode Right) : StatementNode(StartPos, EndPos);
    
    public record ImportFromStatement(UInt32 StartPos, UInt32 EndPos, 
        Token Symbol1, Token[] Dots, StatementNode Left, Token Symbol2, Token Symbol3, StatementNode Right, Token Symbol4) 
        : StatementNode(StartPos, EndPos);
    
    public record ImportAsNameStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol1, Token Symbol2, Token Symbol3) : StatementNode(StartPos, EndPos);
    
    public record DottedAsNameStatement(UInt32 StartPos, UInt32 EndPos, StatementNode Left, Token Symbol1, NameToken Symbol2) : StatementNode(StartPos, EndPos);
    
    public record ImportAsNamesStatement(UInt32 StartPos, UInt32 EndPos, StatementNode[] Nodes, Token[] Separators ) : StatementNode(StartPos, EndPos);
    
    public record DottedAsNamesStatement(UInt32 StartPos, UInt32 EndPos, StatementNode[] Nodes, Token[] Separators) : StatementNode(StartPos, EndPos);
    
    public record DottedNameStatement(UInt32 StartPos, UInt32 EndPos, Token[] Nodes, Token[] Separators) : StatementNode(StartPos, EndPos);
    
    public record GlobalStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol, Token[] Nodes, Token[] Separators) : StatementNode(StartPos, EndPos);
    
    public record NonlocalStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol, Token[] Nodes, Token[] Separators) : StatementNode(StartPos, EndPos);
    
    public record AssertStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol1, ExpressionNode Left, Token Symbol2, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record FuncBodySuiteStatement(UInt32 StartPos, UInt32 EndPos, 
            Token Symbol1, Token Symbol2, Token Symbol3, Token Symbol4, StatementNode[] Nodes, Token Symbol5) 
        : StatementNode(StartPos, EndPos);
    
    public record ClassStatement(UInt32 StartPos, UInt32 EndPos, 
        Token Symbol1, NameToken Symbol2, Token Symbol3, ExpressionNode Left, Token Symbol4, Token Symbol5, StatementNode Right) : StatementNode(StartPos, EndPos);
    
    public record AsyncStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol, StatementNode Right) : StatementNode(StartPos, EndPos);
    
    public record DecoratorStatement(UInt32 StartPos, UInt32 EndPos, 
        Token Symbol, StatementNode Left, Token Symbol2, ExpressionNode Right, Token Symbol3, Token Symbol4) : StatementNode(StartPos, EndPos);
    
    public record DecoratorsStatement(UInt32 StartPos, UInt32 EndPos, StatementNode[] Right) : StatementNode(StartPos, EndPos);
    
    public record DecoratedStatement(UInt32 StartPos, UInt32 EndPos, StatementNode Left, StatementNode Right) : StatementNode(StartPos, EndPos);
    
    public record FuncDefStatement(UInt32 StartPos, UInt32 EndPos, 
        Token Symbol1, Token Symbol2, StatementNode Left, Token Symbol3, Token Symbol4, Token Symbol5, Token Symbol6, StatementNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record ParametersStatement(UInt32 StartPos, UInt32 EndPos, Token Symbol1, StatementNode Right, Token Symbol2) : StatementNode(StartPos, EndPos);
    
    public record TypedArgsListStatement(UInt32 StartPos, UInt32 EndPos, 
        StatementNode[] Nodes, Token[] Separators, Token Slash, Token Mul, ExpressionNode MulNode, Token Power, ExpressionNode PowerNode, Token TypeComment) 
        : StatementNode(StartPos, EndPos);
   
    public record TfpDefStatement(UInt32 StartPos, UInt32 EndPos, 
            Token Symbol1, Token Symbol2, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record TfpDefAssignStatement(UInt32 StartPos, UInt32 EndPos, 
            StatementNode Left, Token Symbol, ExpressionNode Right) 
        : StatementNode(StartPos, EndPos);
    
    public record VarArgsListStatement(UInt32 StartPos, UInt32 EndPos, 
            ExpressionNode[] Nodes, Token[] Separators, Token Slash, Token Mul, NameToken MulNode, Token Power, NameToken PowerNode) 
        : ExpressionNode(StartPos, EndPos);
    
    public record VfpDefAssignStatement(UInt32 StartPos, UInt32 EndPos, 
            NameToken Left, Token Symbol, ExpressionNode Right) 
        : ExpressionNode(StartPos, EndPos);

    public record TypeNode(UInt32 StartPos, UInt32 EndPos) : Node(StartPos, EndPos);
    
    public record TypeInput(UInt32 StartPos, UInt32 EndPos, TypeNode Right, Token[] Newlines, Token Eof) : TypeNode(StartPos, EndPos);
    
    public record FuncType(UInt32 StartPos, UInt32 EndPos, Token Symbol1, TypeNode Left, Token Symbol2, Token Symbol3, ExpressionNode Right) : 
        TypeNode(StartPos, EndPos);
    
    public record TypeList(UInt32 StartPos, UInt32 EndPos, ExpressionNode[] Nodes, Token[] Separators, 
            Token Mul, ExpressionNode MulNode, Token Power, ExpressionNode PowerNode) 
        : TypeNode(StartPos, EndPos);

    public record SingleInputNode(UInt32 StartPos, UInt32 EndPos, Token Newline, StatementNode Right) : StatementNode(StartPos, EndPos);
    
    public record FileInputNode(UInt32 StartPos, UInt32 EndPos, Token[] Newlines, StatementNode[] Nodes, Token Eof) : StatementNode(StartPos, EndPos);
    
    public record EvalInputNode(UInt32 StartPos, UInt32 EndPos, ExpressionNode Right, Token[] Newlines, Token Eof) : StatementNode(StartPos, EndPos);
}
