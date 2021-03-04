
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

        }
    }
}