
using Xunit;
using PythonCoreConcept.Parser;
using PythonCoreConcept.Parser.AST;

namespace TestPythonCoreConcept
{
    public class TestPythonCoreParserFunctional
    {
        [Fact]
        public void EmptyTest()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("() -> x".ToCharArray()));
            var rootNode = parser.ParseFuncTypeInput();
            Assert.True(rootNode is TypeInput);
            var node = (rootNode as TypeInput);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.Equal(0u, node.StartPos);
            Assert.Equal(7u, node.EndPos);
            Assert.True(node.Newlines.Length == 0);
            Assert.True(node.Right is FuncType);
            var node0 = (node.Right as FuncType);
            Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            Assert.Equal(TokenKind.PyArrow, node0.Symbol3.Kind);
            Assert.True(node0.Left == null);
            Assert.True(node0.Right is AtomName);
        }
        
        [Fact]
        public void TestFuncInputWithParameters1()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("(a) -> x".ToCharArray()));
            var rootNode = parser.ParseFuncTypeInput();
            Assert.True(rootNode is TypeInput);
            var node = (rootNode as TypeInput);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.Equal(0u, node.StartPos);
            Assert.Equal(8u, node.EndPos);
            Assert.True(node.Newlines.Length == 0);
            Assert.True(node.Right is FuncType);
            var node0 = (node.Right as FuncType);
            Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            Assert.Equal(TokenKind.PyArrow, node0.Symbol3.Kind);
            Assert.True(node0.Left is TypeList);
            var node1 = (node0.Left as TypeList);
            Assert.True(node1.Nodes.Length == 1);
            Assert.True(node1.Nodes[0] is AtomName);
            
            Assert.True(node0.Right is AtomName);
        }
        
    }
}