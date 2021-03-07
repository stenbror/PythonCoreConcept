
using System.Linq;
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("False".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("None".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("True".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("...".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("__init__".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("45.7e-3J".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("'Hello, World!'".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("'Hello, World!''ax'".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("()".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("[]".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("{}".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("( yield from a )".ToArray()));
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
            public void TestAtomEmptyTupleTestListComp()
            {
                var parser = new PythonCoreParser(new PythonCoreTokenizer("( a,  )".ToArray()));
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
                var parser = new PythonCoreParser(new PythonCoreTokenizer("[ a,  ]".ToArray()));
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
            
            
        }
    }
}