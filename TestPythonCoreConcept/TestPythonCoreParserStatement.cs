
using Xunit;
using PythonCoreConcept.Parser;
using PythonCoreConcept.Parser.AST;

namespace TestPythonCoreConcept
{
    public class TestPythonCoreParserStatement
    {
        [Fact]
        public void TestSingleSimpleStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("pass\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is SimpleStatement);
            var node0 = (node.Nodes[0] as SimpleStatement);
            Assert.Equal(TokenKind.Newline, node0.Symbol.Kind);
            Assert.True(node0.Nodes.Length == 1);
            Assert.True(node0.Separators.Length == 0);
            Assert.True(node0.Nodes[0] is PassStatement);
            var node1 = (node0.Nodes[0] as PassStatement);
            Assert.Equal(TokenKind.PyPass, node1.Symbol.Kind);
        }
        
        [Fact]
        public void TestMultipleButEmptySimpleStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("pass;\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is SimpleStatement);
            var node0 = (node.Nodes[0] as SimpleStatement);
            Assert.Equal(TokenKind.Newline, node0.Symbol.Kind);
            Assert.True(node0.Nodes.Length == 1);
            Assert.True(node0.Separators.Length == 1);
            Assert.True(node0.Nodes[0] is PassStatement);
            var node1 = (node0.Nodes[0] as PassStatement);
            Assert.Equal(TokenKind.PyPass, node1.Symbol.Kind);
        }
        
        [Fact]
        public void TestMultipleSimpleStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("pass;pass\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is SimpleStatement);
            var node0 = (node.Nodes[0] as SimpleStatement);
            Assert.Equal(TokenKind.Newline, node0.Symbol.Kind);
            Assert.True(node0.Nodes.Length == 2);
            Assert.True(node0.Separators.Length == 1);
            Assert.True(node0.Nodes[0] is PassStatement);
            var node1 = (node0.Nodes[0] as PassStatement);
            Assert.Equal(TokenKind.PyPass, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 4u);
            Assert.True(node0.Nodes[1] is PassStatement);
            var node2 = (node0.Nodes[1] as PassStatement);
            Assert.Equal(TokenKind.PyPass, node2.Symbol.Kind);
            Assert.True(node2.StartPos == 5u);
            Assert.True(node2.EndPos == 9u);
        }
        
        [Fact]
        public void TestMultipleSimpleStmtWithNewlines()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("pass;\npass\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 2);
            Assert.True(node.Nodes[0] is SimpleStatement);
            var node0 = (node.Nodes[0] as SimpleStatement);
            Assert.Equal(TokenKind.Newline, node0.Symbol.Kind);
            Assert.True(node0.Separators.Length == 1);
            Assert.True(node.Nodes[1] is SimpleStatement);
            var node1 = (node.Nodes[1] as SimpleStatement);
            Assert.Equal(TokenKind.Newline, node1.Symbol.Kind);
            Assert.True(node1.Separators.Length == 0);
        }
    }
}