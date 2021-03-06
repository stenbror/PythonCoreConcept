
using Xunit;
using PythonCoreConcept.Parser;
using PythonCoreConcept.Parser.AST;

namespace TestPythonCoreConcept
{
    public class TestPythonCoreParser
    {
        public class TestPythonCoreTokenizer
        {
            [Fact]
            public void TestAtomFalse()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("False".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomFalse);
                var node0 = (node as AtomFalse).Symbol;
                Assert.Equal(TokenKind.PyFalse, node0.Kind);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
            }
            
            [Fact]
            public void TestAtomNone()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("None".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomNone);
                var node0 = (node as AtomNone).Symbol;
                Assert.Equal(TokenKind.PyNone, node0.Kind);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(4u, node0.EndPos);
            }
            
            [Fact]
            public void TestAtomTrue()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("True".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomTrue);
                var node0 = (node as AtomTrue).Symbol;
                Assert.Equal(TokenKind.PyTrue, node0.Kind);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(4u, node0.EndPos);
            }
            
            [Fact]
            public void TestAtomElipsis()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("...".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomElipsis);
                var node0 = (node as AtomElipsis).Symbol;
                Assert.Equal(TokenKind.PyElipsis, node0.Kind);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(3u, node0.EndPos);
            }
            
            [Fact]
            public void TestAtomName()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("__init__".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomName);
                var node0 = (node as AtomName).Symbol;
                Assert.Equal(TokenKind.Name, node0.Kind);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(8u, node0.EndPos);
                Assert.Equal("__init__", node0.Text);
            }
            
            [Fact]
            public void TestAtomNumber()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("45.7e-3J".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomNumber);
                var node0 = (node as AtomNumber).Symbol;
                Assert.Equal(TokenKind.Number, node0.Kind);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(8u, node0.EndPos);
                Assert.Equal("45.7e-3J", node0.Text);
            }
            
            [Fact]
            public void TestAtomSingleString()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("'Hello, World!'".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomString);
                var node0 = (node as AtomString).Symbol;
                Assert.Equal(TokenKind.String, node0[0].Kind);
                Assert.Equal(0u, node0[0].StartPos);
                Assert.Equal(15u, node0[0].EndPos);
                Assert.Equal("'Hello, World!'", node0[0].Text);
            }
            
            [Fact]
            public void TestAtomMultipleString()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("'Hello, World!''ax'".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomString);
                var node0 = (node as AtomString).Symbol;
                Assert.Equal(TokenKind.String, node0[0].Kind);
                Assert.Equal(0u, node0[0].StartPos);
                Assert.Equal(15u, node0[0].EndPos);
                Assert.Equal("'Hello, World!'", node0[0].Text);
                
                Assert.Equal(TokenKind.String, node0[1].Kind);
                Assert.Equal(15u, node0[1].StartPos);
                Assert.Equal(19u, node0[1].EndPos);
                Assert.Equal("'ax'", node0[1].Text);
            }
            
            [Fact]
            public void TestAtomEmptyTuple()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("()".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomTuple);
                var node0 = (node as AtomTuple);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(2u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
                Assert.True(node0.Right == null);
                Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomEmptyList()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("[]".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomList);
                var node0 = (node as AtomList);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(2u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftBracket, node0.Symbol1.Kind);
                Assert.True(node0.Right == null);
                Assert.Equal(TokenKind.PyRightBracket, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomEmptyCurly()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{}".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomDictionary);
                var node0 = (node as AtomDictionary);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(2u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftCurly, node0.Symbol1.Kind);
                Assert.True(node0.Right == null);
                Assert.Equal(TokenKind.PyRightCurly, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomEmptyTupleYieldFrom()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("( yield from a )".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomTuple);
                var node0 = (node as AtomTuple);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(16u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
                Assert.True(node0.Right is YieldFrom);
                var node1 = (node0.Right as YieldFrom);
                Assert.Equal(TokenKind.PyYield, node1.Symbol1.Kind);
                Assert.Equal(TokenKind.PyFrom, node1.Symbol2.Kind);
                Assert.Equal(2u, node1.StartPos);
                Assert.Equal(15u, node1.EndPos);
                Assert.True(node1.Right is AtomName);
                Assert.Equal("a", (node1.Right as AtomName).Symbol.Text);
                Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomEmptyTupleYield()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("( yield a, b )".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomTuple);
                var node0 = (node as AtomTuple);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(14u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
                Assert.True(node0.Right is YieldExpr);
                var node1 = (node0.Right as YieldExpr);
                Assert.Equal(TokenKind.PyYield, node1.Symbol1.Kind);
                Assert.Equal(2u, node1.StartPos);
                Assert.Equal(13u, node1.EndPos);
                Assert.True(node1.Right is TestListStarExprStatement);
                var node2 = (node1.Right as TestListStarExprStatement);
                Assert.True(node2.Nodes.Length == 2);
                Assert.True(node2.Separators.Length == 1);
                Assert.True(node2.Nodes[0] is AtomName);
                Assert.True(node2.Nodes[1] is AtomName);
                Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomEmptyTupleSingleEntry()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("( a )".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomTuple);
                var node0 = (node as AtomTuple);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
                Assert.True(node0.Right is AtomName);
                var node1 = (node0.Right as AtomName);
                Assert.Equal("a", node1.Symbol.Text);
                Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomEmptyTupleFor()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("( a for b in c )".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomTuple);
                var node0 = (node as AtomTuple);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(16u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
                Assert.True(node0.Right is TestListComp);
                var node1 = (node0.Right as TestListComp);
                Assert.True(node1.Separators.Length == 0);
                Assert.True(node1.Nodes.Length == 2);
                Assert.True(node1.Nodes[0] is AtomName);
                var node2 = (node1.Nodes[1] as SyncCompFor);
                Assert.Equal(TokenKind.PyFor, node2.Symbol1.Kind);
                Assert.Equal(TokenKind.PyIn, node2.Symbol2.Kind);
                Assert.True(node2.Left is AtomName);
                Assert.True(node2.Right is AtomName);
                Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomEmptyTupleForIf()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("( a for b in c if d )".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomTuple);
                var node0 = (node as AtomTuple);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(21u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
                Assert.True(node0.Right is TestListComp);
                var node1 = (node0.Right as TestListComp);
                Assert.True(node1.Separators.Length == 0);
                Assert.True(node1.Nodes.Length == 2);
                Assert.True(node1.Nodes[0] is AtomName);
                var node2 = (node1.Nodes[1] as SyncCompFor);
                Assert.Equal(TokenKind.PyFor, node2.Symbol1.Kind);
                Assert.Equal(TokenKind.PyIn, node2.Symbol2.Kind);
                Assert.True(node2.Left is AtomName);
                Assert.True(node2.Right is AtomName);
                Assert.True(node2.Next is CompIf);
                var node3 = (node2.Next as CompIf);
                Assert.Equal(TokenKind.PyIf, node3.Symbol1.Kind);
                Assert.True(node3.Right is AtomName);
                Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomEmptyTupleAsyncForIf()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("( a async for b in c if d )".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomTuple);
                var node0 = (node as AtomTuple);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(27u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
                Assert.True(node0.Right is TestListComp);
                var node1 = (node0.Right as TestListComp);
                Assert.True(node1.Separators.Length == 0);
                Assert.True(node1.Nodes.Length == 2);
                Assert.True(node1.Nodes[0] is AtomName);
                var node2 = (node1.Nodes[1] as CompFor);
                Assert.Equal(TokenKind.PyAsync, node2.Symbol1.Kind);
                Assert.True(node2.Right is SyncCompFor);
                Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomEmptyTupleTestListComp()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("( a,  )".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomTuple);
                var node0 = (node as AtomTuple);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(7u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
                Assert.True(node0.Right is TestListComp);
                var node1 = (node0.Right as TestListComp);
                Assert.True(node1.Separators.Length == 1);
                Assert.True(node1.Nodes.Length == 1);
                Assert.Equal(TokenKind.PyComma, node1.Separators[0].Kind);
                Assert.True(node1.Nodes[0] is AtomName);
                Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            }

            [Fact]
            public void TestAtomEmptyListTestListComp()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("[ a,  ]".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomList);
                var node0 = (node as AtomList);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(7u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftBracket, node0.Symbol1.Kind);
                Assert.True(node0.Right is TestListComp);
                var node1 = (node0.Right as TestListComp);
                Assert.True(node1.Separators.Length == 1);
                Assert.True(node1.Nodes.Length == 1);
                Assert.Equal(TokenKind.PyComma, node1.Separators[0].Kind);
                Assert.True(node1.Nodes[0] is AtomName);
                Assert.Equal(TokenKind.PyRightBracket, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomSet()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{ a , b, c }".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomSet);
                var node0 = (node as AtomSet);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(12u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftCurly, node0.Symbol1.Kind);
                Assert.True(node0.Right is SetContainer);
                var node1 = (node0.Right as SetContainer);
                Assert.True(node1.Nodes.Length == 3);
                Assert.True(node1.Separators.Length == 2);
                Assert.True(node1.Nodes[0] is AtomName);
                Assert.True(node1.Nodes[1] is AtomName);
                Assert.True(node1.Nodes[2] is AtomName);
                Assert.Equal(TokenKind.PyRightCurly, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomSetWithMul()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{ a , *b, c }".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomSet);
                var node0 = (node as AtomSet);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(13u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftCurly, node0.Symbol1.Kind);
                Assert.True(node0.Right is SetContainer);
                var node1 = (node0.Right as SetContainer);
                Assert.True(node1.Nodes.Length == 3);
                Assert.True(node1.Separators.Length == 2);
                Assert.True(node1.Nodes[0] is AtomName);
                Assert.True(node1.Nodes[1] is StarExpr);
                var node2 = (node1.Nodes[1] as StarExpr);
                Assert.Equal(TokenKind.PyMul, node2.Symbol.Kind);
                Assert.True(node2.Right is AtomName);
                Assert.True(node1.Nodes[2] is AtomName);
                Assert.Equal(TokenKind.PyRightCurly, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomSetWithTrailingComma()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{ *a , }".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomSet);
                var node0 = (node as AtomSet);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(8u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftCurly, node0.Symbol1.Kind);
                Assert.True(node0.Right is SetContainer);
                var node1 = (node0.Right as SetContainer);
                Assert.True(node1.Nodes.Length == 1);
                Assert.True(node1.Separators.Length == 1);
                Assert.True(node1.Nodes[0] is StarExpr);
                var node2 = (node1.Nodes[0] as StarExpr);
                Assert.Equal(TokenKind.PyMul, node2.Symbol.Kind);
                Assert.True(node2.Right is AtomName);
                Assert.Equal(TokenKind.PyRightCurly, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomSetWithFor()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{ *a for a, b, in c }".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomSet);
                var node0 = (node as AtomSet);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(21u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftCurly, node0.Symbol1.Kind);
                Assert.True(node0.Right is SetContainer);
                var node1 = (node0.Right as SetContainer);
                Assert.True(node1.Nodes.Length == 2);
                Assert.True(node1.Separators.Length == 0);
                Assert.True(node1.Nodes[0] is StarExpr);
                var node2 = (node1.Nodes[0] as StarExpr);
                Assert.Equal(TokenKind.PyMul, node2.Symbol.Kind);
                Assert.True(node2.Right is AtomName);
                
                Assert.True(node1.Nodes[1] is SyncCompFor);
                var node3 = (node1.Nodes[1] as SyncCompFor);
                Assert.True(node3.Left is ExprList);
                var node4 = (node3.Left as ExprList);
                Assert.True(node4.Nodes.Length == 2);
                Assert.True(node4.Separators.Length == 2);
                Assert.True(node4.Nodes[0] is AtomName);
                Assert.True(node4.Nodes[1] is AtomName);
                
                Assert.Equal(TokenKind.PyRightCurly, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomSetWithAsync()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{ *a async for a, b, in c }".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomSet);
                var node0 = (node as AtomSet);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(27u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftCurly, node0.Symbol1.Kind);
                Assert.True(node0.Right is SetContainer);
                var node1 = (node0.Right as SetContainer);
                Assert.True(node1.Nodes.Length == 2);
                Assert.True(node1.Separators.Length == 0);
                Assert.True(node1.Nodes[0] is StarExpr);
                var node2 = (node1.Nodes[0] as StarExpr);
                Assert.Equal(TokenKind.PyMul, node2.Symbol.Kind);
                Assert.True(node2.Right is AtomName);
                Assert.True(node1.Nodes[1] is CompFor);
                Assert.Equal(TokenKind.PyRightCurly, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomDictionary()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{ a : b , c : d, e : f }".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomDictionary);
                var node0 = (node as AtomDictionary);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(24u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftCurly, node0.Symbol1.Kind);
                Assert.True(node0.Right is DictionaryContainer);
                var node1 = (node0.Right as DictionaryContainer);
                Assert.True(node1.Entries.Length == 3);
                Assert.True(node1.Separators.Length == 2);
                Assert.True(node1.Entries[0] is DictionaryEntry);
                Assert.True(node1.Entries[1] is DictionaryEntry);
                Assert.True(node1.Entries[2] is DictionaryEntry);
                var node2 = (node1.Entries[2] as DictionaryEntry);
                Assert.True(node2.Key is AtomName);
                Assert.Equal(TokenKind.PyColon,node2.Symbol.Kind);
                Assert.True(node2.Value is AtomName);
                Assert.Equal(TokenKind.PyRightCurly, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomDictionaryWithPower()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{ a : b , **c }".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomDictionary);
                var node0 = (node as AtomDictionary);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(15u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftCurly, node0.Symbol1.Kind);
                Assert.True(node0.Right is DictionaryContainer);
                var node1 = (node0.Right as DictionaryContainer);
                Assert.True(node1.Entries.Length == 2);
                Assert.True(node1.Separators.Length == 1);
                Assert.True(node1.Entries[0] is DictionaryEntry);
                Assert.True(node1.Entries[1] is DictionaryKW);
                var node2 = (node1.Entries[1] as DictionaryKW);
                Assert.Equal(TokenKind.PyPower, node2.Symbol.Kind);
                Assert.True(node2.Right is AtomName);
                Assert.Equal(TokenKind.PyRightCurly, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomDictionaryWithPowerAndTrailingComma()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{ **a, }".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomDictionary);
                var node0 = (node as AtomDictionary);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(8u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftCurly, node0.Symbol1.Kind);
                Assert.True(node0.Right is DictionaryContainer);
                var node1 = (node0.Right as DictionaryContainer);
                Assert.True(node1.Entries.Length == 1);
                Assert.True(node1.Separators.Length == 1);
                var node2 = (node1.Entries[0] as DictionaryKW);
                Assert.Equal(TokenKind.PyPower, node2.Symbol.Kind);
                Assert.True(node2.Right is AtomName);
                Assert.Equal(TokenKind.PyRightCurly, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomDictionaryWithPowerAndFor()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{ **a for a in b }".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomDictionary);
                var node0 = (node as AtomDictionary);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(18u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftCurly, node0.Symbol1.Kind);
                Assert.True(node0.Right is DictionaryContainer);
                var node1 = (node0.Right as DictionaryContainer);
                Assert.True(node1.Entries.Length == 2);
                Assert.True(node1.Separators.Length == 0);
                var node2 = (node1.Entries[0] as DictionaryKW);
                Assert.Equal(TokenKind.PyPower, node2.Symbol.Kind);
                Assert.True(node2.Right is AtomName);
                Assert.True(node1.Entries[1] is SyncCompFor);
                Assert.Equal(TokenKind.PyRightCurly, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomDictionaryWithPowerAndAsyncFor()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{ **a async for a in b }".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomDictionary);
                var node0 = (node as AtomDictionary);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(24u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftCurly, node0.Symbol1.Kind);
                Assert.True(node0.Right is DictionaryContainer);
                var node1 = (node0.Right as DictionaryContainer);
                Assert.True(node1.Entries.Length == 2);
                Assert.True(node1.Separators.Length == 0);
                var node2 = (node1.Entries[0] as DictionaryKW);
                Assert.Equal(TokenKind.PyPower, node2.Symbol.Kind);
                Assert.True(node2.Right is AtomName);
                Assert.True(node1.Entries[1] is CompFor);
                Assert.Equal(TokenKind.PyRightCurly, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomSetWithNamedExpr()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("( a , b, c := d )".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomTuple);
                var node0 = (node as AtomTuple);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(17u, node0.EndPos);
                Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
                Assert.True(node0.Right is TestListComp);
                var node1 = (node0.Right as TestListComp);
                Assert.True(node1.Nodes.Length == 3);
                Assert.True(node1.Separators.Length == 2);
                Assert.True(node1.Nodes[0] is AtomName);
                Assert.True(node1.Nodes[1] is AtomName);
                Assert.True(node1.Nodes[2] is NamedExpr);
                Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameDotName()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a.b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(3u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is DotName);
                var node1 = (node0.Right[0] as DotName);
                Assert.Equal(TokenKind.PyDot, node1.Symbol.Kind);
                Assert.Equal(TokenKind.Name, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameDotNameDotName()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a.b.c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is DotName);
                var node1 = (node0.Right[0] as DotName);
                Assert.Equal(TokenKind.PyDot, node1.Symbol.Kind);
                Assert.Equal(TokenKind.Name, node1.Symbol2.Kind);
                Assert.True(node0.Right[1] is DotName);
            }
            
            [Fact]
            public void TestAtomNameCallEmpty()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a()".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(3u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Call);
                var node1 = (node0.Right[0] as Call);
                Assert.Equal(TokenKind.PyLeftParen, node1.Symbol1.Kind);
                Assert.True(node1.Right == null);
                Assert.Equal(TokenKind.PyRightParen, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameCallWithArg1()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a(b, c,)".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(8u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Call);
                var node1 = (node0.Right[0] as Call);
                Assert.Equal(TokenKind.PyLeftParen, node1.Symbol1.Kind);
                Assert.True(node1.Right is ArgList);
                var node2 = (node1.Right as ArgList);
                Assert.True(node2.Nodes.Length == 2);
                Assert.True(node2.Separators.Length == 2);
                Assert.True(node2.Nodes[0] is AtomName);
                Assert.True(node2.Nodes[1] is AtomName);
                Assert.Equal(TokenKind.PyRightParen, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameCallWithArg2()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a(*b, c)".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(8u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Call);
                var node1 = (node0.Right[0] as Call);
                Assert.Equal(TokenKind.PyLeftParen, node1.Symbol1.Kind);
                Assert.True(node1.Right is ArgList);
                var node2 = (node1.Right as ArgList);
                Assert.True(node2.Nodes.Length == 2);
                Assert.True(node2.Separators.Length == 1);
                Assert.True(node2.Nodes[0] is Argument);
                var node3 = (node2.Nodes[0] as Argument);
                Assert.Equal(TokenKind.PyMul, node3.Symbol.Kind);
                Assert.True(node3.Right is AtomName);
                Assert.Equal(TokenKind.PyRightParen, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameCallWithArg3()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a(**b, c)".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(9u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Call);
                var node1 = (node0.Right[0] as Call);
                Assert.Equal(TokenKind.PyLeftParen, node1.Symbol1.Kind);
                Assert.True(node1.Right is ArgList);
                var node2 = (node1.Right as ArgList);
                Assert.True(node2.Nodes.Length == 2);
                Assert.True(node2.Separators.Length == 1);
                Assert.True(node2.Nodes[0] is Argument);
                var node3 = (node2.Nodes[0] as Argument);
                Assert.Equal(TokenKind.PyPower, node3.Symbol.Kind);
                Assert.True(node3.Right is AtomName);
                Assert.Equal(TokenKind.PyRightParen, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameCallWithArg4()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a(b := c, 1)".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(12u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Call);
                var node1 = (node0.Right[0] as Call);
                Assert.Equal(TokenKind.PyLeftParen, node1.Symbol1.Kind);
                Assert.True(node1.Right is ArgList);
                var node2 = (node1.Right as ArgList);
                Assert.True(node2.Nodes.Length == 2);
                Assert.True(node2.Separators.Length == 1);
                Assert.True(node2.Nodes[0] is Argument);
                var node3 = (node2.Nodes[0] as Argument);
                Assert.True(node3.Left is AtomName);
                Assert.Equal(TokenKind.PyColonAssign, node3.Symbol.Kind);
                Assert.True(node3.Right is AtomName);
                Assert.Equal(TokenKind.PyRightParen, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameCallWithArg5()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a(b = c, 1)".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(11u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Call);
                var node1 = (node0.Right[0] as Call);
                Assert.Equal(TokenKind.PyLeftParen, node1.Symbol1.Kind);
                Assert.True(node1.Right is ArgList);
                var node2 = (node1.Right as ArgList);
                Assert.True(node2.Nodes.Length == 2);
                Assert.True(node2.Separators.Length == 1);
                Assert.True(node2.Nodes[0] is Argument);
                var node3 = (node2.Nodes[0] as Argument);
                Assert.True(node3.Left is AtomName);
                Assert.Equal(TokenKind.PyAssign, node3.Symbol.Kind);
                Assert.True(node3.Right is AtomName);
                Assert.Equal(TokenKind.PyRightParen, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameCallWithArg6()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a(b for c in d)".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(15u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Call);
                var node1 = (node0.Right[0] as Call);
                Assert.Equal(TokenKind.PyLeftParen, node1.Symbol1.Kind);
                Assert.True(node1.Right is Argument);
                var node2 = (node1.Right as Argument);
                Assert.True(node2.Left is AtomName);
                Assert.True(node2.Right is SyncCompFor);
                Assert.True(node2.Symbol == null);
                Assert.Equal(TokenKind.PyRightParen, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameCallWithArg7()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a(b async for c in d)".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(21u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Call);
                var node1 = (node0.Right[0] as Call);
                Assert.Equal(TokenKind.PyLeftParen, node1.Symbol1.Kind);
                Assert.True(node1.Right is Argument);
                var node2 = (node1.Right as Argument);
                Assert.True(node2.Left is AtomName);
                Assert.True(node2.Right is CompFor);
                Assert.True(node2.Symbol == null);
                Assert.Equal(TokenKind.PyRightParen, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameIndex1()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a[1]".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(4u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Index);
                var node1 = (node0.Right[0] as Index);
                Assert.Equal(TokenKind.PyLeftBracket, node1.Symbol1.Kind);
                Assert.True(node1.Right is Subscript);
                var node2 = (node1.Right as Subscript);
                Assert.True(node2.First is AtomNumber);
                Assert.True(node2.Symbol1 == null);
                Assert.True(node2.Two == null);
                Assert.True(node2.Symbol2 == null);
                Assert.True(node2.Three == null);
                Assert.Equal(TokenKind.PyRightBracket, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameIndex2()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a[1:10]".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(7u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Index);
                var node1 = (node0.Right[0] as Index);
                Assert.Equal(TokenKind.PyLeftBracket, node1.Symbol1.Kind);
                Assert.True(node1.Right is Subscript);
                var node2 = (node1.Right as Subscript);
                Assert.True(node2.First is AtomNumber);
                Assert.Equal(TokenKind.PyColon, node2.Symbol1.Kind);
                Assert.True(node2.Two is AtomNumber);
                Assert.True(node2.Symbol2 == null);
                Assert.True(node2.Three == null);
                Assert.Equal(TokenKind.PyRightBracket, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameIndex3()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a[1:10:5]".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(9u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Index);
                var node1 = (node0.Right[0] as Index);
                Assert.Equal(TokenKind.PyLeftBracket, node1.Symbol1.Kind);
                Assert.True(node1.Right is Subscript);
                var node2 = (node1.Right as Subscript);
                Assert.True(node2.First is AtomNumber);
                Assert.Equal(TokenKind.PyColon, node2.Symbol1.Kind);
                Assert.True(node2.Two is AtomNumber);
                Assert.Equal(TokenKind.PyColon, node2.Symbol2.Kind);
                Assert.True(node2.Three is AtomNumber);
                Assert.Equal(TokenKind.PyRightBracket, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameIndex4()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a[:10:5]".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(8u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Index);
                var node1 = (node0.Right[0] as Index);
                Assert.Equal(TokenKind.PyLeftBracket, node1.Symbol1.Kind);
                Assert.True(node1.Right is Subscript);
                var node2 = (node1.Right as Subscript);
                Assert.True(node2.First == null);
                Assert.Equal(TokenKind.PyColon, node2.Symbol1.Kind);
                Assert.True(node2.Two is AtomNumber);
                Assert.Equal(TokenKind.PyColon, node2.Symbol2.Kind);
                Assert.True(node2.Three is AtomNumber);
                Assert.Equal(TokenKind.PyRightBracket, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameIndex5()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a[::5]".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Index);
                var node1 = (node0.Right[0] as Index);
                Assert.Equal(TokenKind.PyLeftBracket, node1.Symbol1.Kind);
                Assert.True(node1.Right is Subscript);
                var node2 = (node1.Right as Subscript);
                Assert.True(node2.First == null);
                Assert.Equal(TokenKind.PyColon, node2.Symbol1.Kind);
                Assert.True(node2.Two == null);
                Assert.Equal(TokenKind.PyColon, node2.Symbol2.Kind);
                Assert.True(node2.Three is AtomNumber);
                Assert.Equal(TokenKind.PyRightBracket, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameIndex6()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a[::]".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Index);
                var node1 = (node0.Right[0] as Index);
                Assert.Equal(TokenKind.PyLeftBracket, node1.Symbol1.Kind);
                Assert.True(node1.Right is Subscript);
                var node2 = (node1.Right as Subscript);
                Assert.True(node2.First == null);
                Assert.Equal(TokenKind.PyColon, node2.Symbol1.Kind);
                Assert.True(node2.Two == null);
                Assert.Equal(TokenKind.PyColon, node2.Symbol2.Kind);
                Assert.True(node2.Three == null);
                Assert.Equal(TokenKind.PyRightBracket, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameIndex7()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a[:]".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(4u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Index);
                var node1 = (node0.Right[0] as Index);
                Assert.Equal(TokenKind.PyLeftBracket, node1.Symbol1.Kind);
                Assert.True(node1.Right is Subscript);
                var node2 = (node1.Right as Subscript);
                Assert.True(node2.First == null);
                Assert.Equal(TokenKind.PyColon, node2.Symbol1.Kind);
                Assert.True(node2.Two == null);
                Assert.True(node2.Symbol2 == null);
                Assert.True(node2.Three == null);
                Assert.Equal(TokenKind.PyRightBracket, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameIndex8()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a[1:10, 2, 3, ]".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(15u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Index);
                var node1 = (node0.Right[0] as Index);
                Assert.Equal(TokenKind.PyLeftBracket, node1.Symbol1.Kind);
                Assert.True(node1.Right is SubscriptList);
                var node2 = (node1.Right as SubscriptList);
                Assert.True(node2.Nodes.Length == 3);
                Assert.True(node2.Separators.Length == 3);
                Assert.True(node2.Nodes[0] is Subscript);
                Assert.True(node2.Nodes[1] is Subscript);
                Assert.True(node2.Nodes[2] is Subscript);
                Assert.Equal(TokenKind.PyRightBracket, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestAtomNameIndex9()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("await a[1]".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AtomExpr);
                var node0 = (node as AtomExpr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(10u, node0.EndPos);
                Assert.Equal(TokenKind.PyAwait, node0.Symbol.Kind);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right[0] is Index);
                var node1 = (node0.Right[0] as Index);
                Assert.Equal(TokenKind.PyLeftBracket, node1.Symbol1.Kind);
                Assert.True(node1.Right is Subscript);
                var node2 = (node1.Right as Subscript);
                Assert.True(node2.First is AtomNumber);
                Assert.True(node2.Symbol1 == null);
                Assert.True(node2.Two == null);
                Assert.True(node2.Symbol2 == null);
                Assert.True(node2.Three == null);
                Assert.Equal(TokenKind.PyRightBracket, node1.Symbol2.Kind);
            }
            
            [Fact]
            public void TestPowerSingle()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a ** b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Power);
                var node0 = (node as Power);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestPowerMultiple()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a ** b ** c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Power);
                var node0 = (node as Power);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(11u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is Power);
                var node1 = (node0.Right as Power);
                Assert.True(node1.Left is AtomName);
                Assert.True(node1.Right is AtomName);
            }
            
            [Fact]
            public void TestFactorSinglePlus()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("+a".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is UnaryPlus);
                var node0 = (node as UnaryPlus);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(2u, node0.EndPos);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestFactorSingleMinus()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("-a".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is UnaryMinus);
                var node0 = (node as UnaryMinus);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(2u, node0.EndPos);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestFactorSingleBitInvert()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("~a".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is UnaryBitInvert);
                var node0 = (node as UnaryBitInvert);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(2u, node0.EndPos);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestFactorMultipleMinus()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("--a".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is UnaryMinus);
                var node0 = (node as UnaryMinus);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(3u, node0.EndPos);
                Assert.True(node0.Right is UnaryMinus);
            }
            
            [Fact]
            public void TestTermSingleMul()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a * b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Mul);
                var node0 = (node as Mul);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestTermSingleMatrice()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a @ b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Matrice);
                var node0 = (node as Matrice);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestTermSingleDiv()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a / b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Div);
                var node0 = (node as Div);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestTermSingleModulo()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a % b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Modulo);
                var node0 = (node as Modulo);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestTermSingleFloorDiv()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a // b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is FloorDiv);
                var node0 = (node as FloorDiv);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestTermMultipleDiv()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a / b * c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Mul);
                var node0 = (node as Mul);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(9u, node0.EndPos);
                Assert.True(node0.Left is Div);
                Assert.True(node0.Right is AtomName);
                var node1 = (node0.Left as Div);
                Assert.Equal(0u, node1.StartPos);
                Assert.Equal(6u, node1.EndPos);
                Assert.True(node1.Left is AtomName);
                Assert.True(node1.Right is AtomName);
            }
            
            [Fact]
            public void TestTArithSinglePlus()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a + b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Plus);
                var node0 = (node as Plus);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestTArithSingleMinus()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a - b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Minus);
                var node0 = (node as Minus);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestArithMultiplePlusMinus()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a + b - c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Minus);
                var node0 = (node as Minus);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(9u, node0.EndPos);
                Assert.True(node0.Left is Plus);
                Assert.True(node0.Right is AtomName);
                var node1 = (node0.Left as Plus);
                Assert.Equal(0u, node1.StartPos);
                Assert.Equal(6u, node1.EndPos);
                Assert.True(node1.Left is AtomName);
                Assert.True(node1.Right is AtomName);
            }
            
            [Fact]
            public void TestTShiftSingleLeft()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a << b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is ShiftLeft);
                var node0 = (node as ShiftLeft);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestTShiftSingleRight()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a >> b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is ShiftRight);
                var node0 = (node as ShiftRight);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestShiftMultipleLeftRight()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a << b >> c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is ShiftRight);
                var node0 = (node as ShiftRight);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(11u, node0.EndPos);
                Assert.True(node0.Left is ShiftLeft);
                Assert.True(node0.Right is AtomName);
                var node1 = (node0.Left as ShiftLeft);
                Assert.Equal(0u, node1.StartPos);
                Assert.Equal(7u, node1.EndPos);
                Assert.True(node1.Left is AtomName);
                Assert.True(node1.Right is AtomName);
            }
            
            [Fact]
            public void TestSingleBitAnd()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a & b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is BitAnd);
                var node0 = (node as BitAnd);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestMultipleBitAnd()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a & b & c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is BitAnd);
                var node0 = (node as BitAnd);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(9u, node0.EndPos);
                Assert.True(node0.Left is BitAnd);
                Assert.True(node0.Right is AtomName);
                var node1 = (node0.Left as BitAnd);
                Assert.True(node1.Left is AtomName);
                Assert.True(node1.Right is AtomName);
                Assert.Equal(0u, node1.StartPos);
                Assert.Equal(6u, node1.EndPos);
            }
            
            [Fact]
            public void TestSingleBitXor()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a ^ b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is BitXor);
                var node0 = (node as BitXor);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestMultipleBitXor()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a ^ b ^ c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is BitXor);
                var node0 = (node as BitXor);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(9u, node0.EndPos);
                Assert.True(node0.Left is BitXor);
                Assert.True(node0.Right is AtomName);
                var node1 = (node0.Left as BitXor);
                Assert.True(node1.Left is AtomName);
                Assert.True(node1.Right is AtomName);
                Assert.Equal(0u, node1.StartPos);
                Assert.Equal(6u, node1.EndPos);
            }
            
            [Fact]
            public void TestSingleBitOr()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a | b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is BitOr);
                var node0 = (node as BitOr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestMultipleBitOr()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a | b | c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is BitOr);
                var node0 = (node as BitOr);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(9u, node0.EndPos);
                Assert.True(node0.Left is BitOr);
                Assert.True(node0.Right is AtomName);
                var node1 = (node0.Left as BitOr);
                Assert.True(node1.Left is AtomName);
                Assert.True(node1.Right is AtomName);
                Assert.Equal(0u, node1.StartPos);
                Assert.Equal(6u, node1.EndPos);
            }
            
            [Fact]
            public void TestCompareSingleLess()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a < b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareLess);
                var node0 = (node as CompareLess);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestCompareSingleGreater()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a > b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareGreater);
                var node0 = (node as CompareGreater);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestCompareSingleLessEqual()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a <= b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareLessEqual);
                var node0 = (node as CompareLessEqual);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestCompareSingleGreaterEqual()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a >= b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareGreaterEqual);
                var node0 = (node as CompareGreaterEqual);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestCompareSingleEqual()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a == b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareEqual);
                var node0 = (node as CompareEqual);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestCompareSingleNotEqual()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a != b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareNotEqual);
                var node0 = (node as CompareNotEqual);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestCompareSingleNotEqualLegacy()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a <> b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareNotEqual);
                var node0 = (node as CompareNotEqual);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestCompareSingleIn()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a in b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareIn);
                var node0 = (node as CompareIn);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestCompareSingleIs()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a is b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareIs);
                var node0 = (node as CompareIs);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestCompareSingleIsNot()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a is not b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareIsNot);
                var node0 = (node as CompareIsNot);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(10u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestCompareSingleNotIn()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a not in b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareNotIn);
                var node0 = (node as CompareNotIn);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(10u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestCompareMultipleLessGreater()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a < b > c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is CompareGreater);
                var node0 = (node as CompareGreater);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(9u, node0.EndPos);
                Assert.True(node0.Left is CompareLess);
                Assert.True(node0.Right is AtomName);
                var node1 = (node0.Left as CompareLess);
                Assert.Equal(0u, node1.StartPos);
                Assert.Equal(6u, node1.EndPos);
                Assert.True(node1.Left is AtomName);
                Assert.True(node1.Right is AtomName);
            }
            
            [Fact]
            public void TestNotTestSingle()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("not a".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is NotTest);
                var node0 = (node as NotTest);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(5u, node0.EndPos);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestNotTestMultiple()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("not not a".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is NotTest);
                var node0 = (node as NotTest);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(9u, node0.EndPos);
                Assert.True(node0.Right is NotTest);
            }
            
            [Fact]
            public void TestAndTestSingle()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a and b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AndTest);
                var node0 = (node as AndTest);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(7u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestAndTestMultiple()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a and b and c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is AndTest);
                var node0 = (node as AndTest);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(13u, node0.EndPos);
                Assert.True(node0.Left is AndTest);
                Assert.True(node0.Right is AtomName);
                var node1 = (node0.Left as AndTest);
                Assert.Equal(0u, node1.StartPos);
                Assert.Equal(8u, node1.EndPos);
                Assert.True(node1.Left is AtomName);
                Assert.True(node1.Right is AtomName);
            }
            
            [Fact]
            public void TestOrTestSingle()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a or b".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is OrTest);
                var node0 = (node as OrTest);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(6u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestOrTestMultiple()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a or b or c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is OrTest);
                var node0 = (node as OrTest);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(11u, node0.EndPos);
                Assert.True(node0.Left is OrTest);
                Assert.True(node0.Right is AtomName);
                var node1 = (node0.Left as OrTest);
                Assert.Equal(0u, node1.StartPos);
                Assert.Equal(7u, node1.EndPos);
                Assert.True(node1.Left is AtomName);
                Assert.True(node1.Right is AtomName);
            }
            
            [Fact]
            public void TestExpressionTest()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a if b else c".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Test);
                var node0 = (node as Test);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(13u, node0.EndPos);
                Assert.True(node0.Left is AtomName);
                Assert.Equal(TokenKind.PyIf, node0.Symbol1.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.Equal(TokenKind.PyElse, node0.Symbol2.Kind);
                Assert.True(node0.Next is AtomName);
            }
            
            [Fact]
            public void TestExpressionEvalInputTestList()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("a, b, c \r\n\r\n".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 2);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is TestList);
                var node0 = (node as TestList);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(8u, node0.EndPos);
                Assert.True(node0.Nodes.Length == 3);
                Assert.True(node0.Nodes[0] is AtomName);
                Assert.True(node0.Nodes[1] is AtomName);
                Assert.True(node0.Nodes[2] is AtomName);
            }
            
            [Fact]
            public void TestLambdaNoParameters()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(9u, node0.EndPos);
                Assert.True(node0.Left == null);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
            }
            
            [Fact]
            public void TestLambdaParameters1()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(11u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 1);
                Assert.True(node1.Nodes[0] is VfpDefAssignStatement);
                var node2 = (node1.Nodes[0] as VfpDefAssignStatement);
                Assert.True(node2.Left is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters2()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a,: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(12u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 1);
                Assert.True(node1.Separators.Length == 1);
                Assert.True(node1.Nodes[0] is VfpDefAssignStatement);
                var node2 = (node1.Nodes[0] as VfpDefAssignStatement);
                Assert.True(node2.Left is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters3()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a, b, c: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(17u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 3);
                Assert.True(node1.Separators.Length == 2);
                Assert.True(node1.Nodes[0] is VfpDefAssignStatement);
                var node2 = (node1.Nodes[0] as VfpDefAssignStatement);
                Assert.True(node2.Left is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters4()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a, b, c, /: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(20u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 3);
                Assert.True(node1.Separators.Length == 3);
                Assert.True(node1.Nodes[0] is VfpDefAssignStatement);
                var node2 = (node1.Nodes[0] as VfpDefAssignStatement);
                Assert.True(node2.Left is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters5()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a, b, c, /, d: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(23u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 4);
                Assert.True(node1.Separators.Length == 4);
                Assert.Equal(TokenKind.PyDiv, node1.Slash.Kind);
                Assert.True(node1.Nodes[0] is VfpDefAssignStatement);
                var node2 = (node1.Nodes[0] as VfpDefAssignStatement);
                Assert.True(node2.Left is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters6()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a, b, c, /, d, *f: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(27u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 4);
                Assert.True(node1.Separators.Length == 5);
                Assert.Equal(TokenKind.PyDiv, node1.Slash.Kind);
                Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
                Assert.True(node1.MulNode is NameToken);
                Assert.True(node1.Nodes[0] is VfpDefAssignStatement);
                var node2 = (node1.Nodes[0] as VfpDefAssignStatement);
                Assert.True(node2.Left is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters7()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a, b, c, /, d, *f, g, h: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(33u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 6);
                Assert.True(node1.Separators.Length == 7);
                Assert.Equal(TokenKind.PyDiv, node1.Slash.Kind);
                Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
                Assert.True(node1.MulNode is NameToken);
                Assert.True(node1.Nodes[0] is VfpDefAssignStatement);
                var node2 = (node1.Nodes[0] as VfpDefAssignStatement);
                Assert.True(node2.Left is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters8()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a, b, c, /, d, *f, g, h, **i: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(38u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 6);
                Assert.True(node1.Separators.Length == 8);
                Assert.Equal(TokenKind.PyDiv, node1.Slash.Kind);
                Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
                Assert.Equal(TokenKind.PyPower, node1.Power.Kind);
                Assert.True(node1.MulNode is NameToken);
                Assert.True(node1.PowerNode is NameToken);
                Assert.True(node1.Nodes[0] is VfpDefAssignStatement);
                var node2 = (node1.Nodes[0] as VfpDefAssignStatement);
                Assert.True(node2.Left is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters9()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a, b, c, /, d, *f, g, h, **i,: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(39u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 6);
                Assert.True(node1.Separators.Length == 9);
                Assert.Equal(TokenKind.PyDiv, node1.Slash.Kind);
                Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
                Assert.Equal(TokenKind.PyPower, node1.Power.Kind);
                Assert.True(node1.MulNode is NameToken);
                Assert.True(node1.PowerNode is NameToken);
                Assert.True(node1.Nodes[0] is VfpDefAssignStatement);
                var node2 = (node1.Nodes[0] as VfpDefAssignStatement);
                Assert.True(node2.Left is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters10()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a = 1, b, c, /, d, *f, g, h, **i,: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(43u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 6);
                Assert.True(node1.Separators.Length == 9);
                Assert.Equal(TokenKind.PyDiv, node1.Slash.Kind);
                Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
                Assert.Equal(TokenKind.PyPower, node1.Power.Kind);
                Assert.True(node1.MulNode is NameToken);
                Assert.True(node1.PowerNode is NameToken);
                Assert.True(node1.Nodes[0] is VfpDefAssignStatement);
                var node2 = (node1.Nodes[0] as VfpDefAssignStatement);
                Assert.True(node2.Left is NameToken);
                Assert.Equal(TokenKind.PyAssign, node2.Symbol.Kind);
                //Assert.True(node2.Right is NumberToken);
            }
            
            [Fact]
            public void TestLambdaParameters11()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a = 1, b, c, /, d, **i,: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(33u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 4);
                Assert.True(node1.Separators.Length == 6);
                Assert.Equal(TokenKind.PyDiv, node1.Slash.Kind);
                Assert.Equal(TokenKind.PyPower, node1.Power.Kind);
                Assert.True(node1.PowerNode is NameToken);
                Assert.True(node1.Nodes[0] is VfpDefAssignStatement);
                var node2 = (node1.Nodes[0] as VfpDefAssignStatement);
                Assert.True(node2.Left is NameToken);
                Assert.Equal(TokenKind.PyAssign, node2.Symbol.Kind);
                //Assert.True(node2.Right is NumberToken);
            }
            
            [Fact]
            public void TestLambdaParameters12()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda **a,: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(14u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 0);
                Assert.True(node1.Separators.Length == 1);
                Assert.True(node1.Slash == null);
                Assert.Equal(TokenKind.PyPower, node1.Power.Kind);
                Assert.True(node1.PowerNode is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters13()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda *a,: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(13u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 0);
                Assert.True(node1.Separators.Length == 1);
                Assert.True(node1.Slash == null);
                Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
                Assert.True(node1.MulNode is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters14()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda *a, b: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(15u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 1);
                Assert.True(node1.Separators.Length == 1);
                Assert.True(node1.Slash == null);
                Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
                Assert.True(node1.MulNode is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters15()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda *a, b, **c,: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(21u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 1);
                Assert.True(node1.Separators.Length == 3);
                Assert.True(node1.Slash == null);
                Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
                Assert.True(node1.MulNode is NameToken);
                Assert.Equal(TokenKind.PyPower, node1.Power.Kind);
                Assert.True(node1.PowerNode is NameToken);
            }
            
            [Fact]
            public void TestLambdaParameters16()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda *a, b, **c: x".ToCharArray()));
                var rootNode = parser.ParseEvalInput();
                Assert.True(rootNode is EvalInputNode);
                Assert.Equal(TokenKind.EndOfFile, (rootNode as EvalInputNode).Eof.Kind);
                Assert.True((rootNode as EvalInputNode).Newlines.Length == 0);
                var node = (rootNode as EvalInputNode).Right;
                Assert.True(node is Lambda);
                var node0 = (node as Lambda);
                Assert.Equal(0u, node0.StartPos);
                Assert.Equal(20u, node0.EndPos);
                Assert.Equal(TokenKind.PyLambda, node0.Symbol1.Kind);
                Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
                Assert.True(node0.Right is AtomName);
                Assert.True(node0.Left is VarArgsListStatement);
                var node1 = (node0.Left as VarArgsListStatement);
                Assert.True(node1.Nodes.Length == 1);
                Assert.True(node1.Separators.Length == 2);
                Assert.True(node1.Slash == null);
                Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
                Assert.True(node1.MulNode is NameToken);
                Assert.Equal(TokenKind.PyPower, node1.Power.Kind);
                Assert.True(node1.PowerNode is NameToken);
            }
        }
    }
}