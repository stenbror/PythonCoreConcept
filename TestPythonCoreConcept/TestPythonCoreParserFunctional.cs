
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
            Assert.True(node1.Separators.Length == 0);
            Assert.True(node1.Nodes[0] is AtomName);
            
            Assert.True(node0.Right is AtomName);
        }
        
        [Fact]
        public void TestFuncInputWithParameters2()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("(a, b) -> x".ToCharArray()));
            var rootNode = parser.ParseFuncTypeInput();
            Assert.True(rootNode is TypeInput);
            var node = (rootNode as TypeInput);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.Equal(0u, node.StartPos);
            Assert.Equal(11u, node.EndPos);
            Assert.True(node.Newlines.Length == 0);
            Assert.True(node.Right is FuncType);
            var node0 = (node.Right as FuncType);
            Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            Assert.Equal(TokenKind.PyArrow, node0.Symbol3.Kind);
            Assert.True(node0.Left is TypeList);
            var node1 = (node0.Left as TypeList);
            Assert.True(node1.Nodes.Length == 2);
            Assert.True(node1.Separators.Length == 1);
            Assert.True(node1.Nodes[0] is AtomName);
            
            Assert.True(node0.Right is AtomName);
        }
        
        [Fact]
        public void TestFuncInputWithParameters3()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("(a, b, *c) -> x".ToCharArray()));
            var rootNode = parser.ParseFuncTypeInput();
            Assert.True(rootNode is TypeInput);
            var node = (rootNode as TypeInput);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.Equal(0u, node.StartPos);
            Assert.Equal(15u, node.EndPos);
            Assert.True(node.Newlines.Length == 0);
            Assert.True(node.Right is FuncType);
            var node0 = (node.Right as FuncType);
            Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            Assert.Equal(TokenKind.PyArrow, node0.Symbol3.Kind);
            Assert.True(node0.Left is TypeList);
            var node1 = (node0.Left as TypeList);
            Assert.True(node1.Nodes.Length == 2);
            Assert.True(node1.Separators.Length == 2);
            Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
            Assert.True(node1.MulNode is AtomName);
            Assert.True(node1.Nodes[0] is AtomName);
            
            Assert.True(node0.Right is AtomName);
        }
        
        [Fact]
        public void TestFuncInputWithParameters4()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("(a, b, *c, d) -> x".ToCharArray()));
            var rootNode = parser.ParseFuncTypeInput();
            Assert.True(rootNode is TypeInput);
            var node = (rootNode as TypeInput);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.Equal(0u, node.StartPos);
            Assert.Equal(18u, node.EndPos);
            Assert.True(node.Newlines.Length == 0);
            Assert.True(node.Right is FuncType);
            var node0 = (node.Right as FuncType);
            Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            Assert.Equal(TokenKind.PyArrow, node0.Symbol3.Kind);
            Assert.True(node0.Left is TypeList);
            var node1 = (node0.Left as TypeList);
            Assert.True(node1.Nodes.Length == 3);
            Assert.True(node1.Separators.Length == 3);
            Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
            Assert.True(node1.MulNode is AtomName);
            Assert.True(node1.Nodes[0] is AtomName);
            
            Assert.True(node0.Right is AtomName);
        }
        
        [Fact]
        public void TestFuncInputWithParameters5()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("(a, b, *c, d, **f) -> x".ToCharArray()));
            var rootNode = parser.ParseFuncTypeInput();
            Assert.True(rootNode is TypeInput);
            var node = (rootNode as TypeInput);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.Equal(0u, node.StartPos);
            Assert.Equal(23u, node.EndPos);
            Assert.True(node.Newlines.Length == 0);
            Assert.True(node.Right is FuncType);
            var node0 = (node.Right as FuncType);
            Assert.Equal(TokenKind.PyLeftParen, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyRightParen, node0.Symbol2.Kind);
            Assert.Equal(TokenKind.PyArrow, node0.Symbol3.Kind);
            Assert.True(node0.Left is TypeList);
            var node1 = (node0.Left as TypeList);
            Assert.True(node1.Nodes.Length == 3);
            Assert.True(node1.Separators.Length == 4);
            Assert.Equal(TokenKind.PyMul, node1.Mul.Kind);
            Assert.True(node1.MulNode is AtomName);
            Assert.Equal(TokenKind.PyPower, node1.Power.Kind);
            Assert.True(node1.PowerNode is AtomName);
            Assert.True(node1.Nodes[0] is AtomName);
            
            Assert.True(node0.Right is AtomName);
        }
        
    }
}