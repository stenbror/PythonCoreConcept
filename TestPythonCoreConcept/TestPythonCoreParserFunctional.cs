
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
            Assert.True(node.Newlines.Length == 0);
            Assert.True(node.Right is FuncType);
            var node0 = (node.Right as FuncType);
            Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            Assert.Equal(TokenKind.PyArrow, node0.Symbol3.Kind);
            Assert.True(node0.Left == null);
            Assert.True(node0.Right is AtomName);
        }
        
    }
}