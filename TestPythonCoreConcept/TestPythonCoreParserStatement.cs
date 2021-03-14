
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
        
        [Fact]
        public void TestMultipleSimpleStmtWithAdditionalNewlines()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("pass;\n\npass\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 1);
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
        
        [Fact]
        public void TestPlusAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a += 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is PlusAssignStatement);
            var node1 = (node0.Nodes[0] as PlusAssignStatement);
            Assert.Equal(TokenKind.PyPlusAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 6u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestMinusAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a -= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is MinusAssignStatement);
            var node1 = (node0.Nodes[0] as MinusAssignStatement);
            Assert.Equal(TokenKind.PyMinusAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 6u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestMulAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a *= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is MulAssignStatement);
            var node1 = (node0.Nodes[0] as MulAssignStatement);
            Assert.Equal(TokenKind.PyMulAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 6u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestDivAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a /= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is DivAssignStatement);
            var node1 = (node0.Nodes[0] as DivAssignStatement);
            Assert.Equal(TokenKind.PyDivAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 6u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestModuloAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a %= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ModuloAssignStatement);
            var node1 = (node0.Nodes[0] as ModuloAssignStatement);
            Assert.Equal(TokenKind.PyModuloAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 6u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestMatriceAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a @= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is MatriceAssignStatement);
            var node1 = (node0.Nodes[0] as MatriceAssignStatement);
            Assert.Equal(TokenKind.PyMatriceAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 6u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestBitAndAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a &= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is BitAndAssignStatement);
            var node1 = (node0.Nodes[0] as BitAndAssignStatement);
            Assert.Equal(TokenKind.PyBitAndAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 6u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestBitOrAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a |= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is BitOrAssignStatement);
            var node1 = (node0.Nodes[0] as BitOrAssignStatement);
            Assert.Equal(TokenKind.PyBitOrAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 6u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestBitXorAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a ^= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is BitXorAssignStatement);
            var node1 = (node0.Nodes[0] as BitXorAssignStatement);
            Assert.Equal(TokenKind.PyBitXorAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 6u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestShiftLeftAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a <<= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ShiftLeftAssignStatement);
            var node1 = (node0.Nodes[0] as ShiftLeftAssignStatement);
            Assert.Equal(TokenKind.PyShiftLeftAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 7u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestShiftRightAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a >>= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ShiftRightAssignStatement);
            var node1 = (node0.Nodes[0] as ShiftRightAssignStatement);
            Assert.Equal(TokenKind.PyShiftRightAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 7u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestPowerAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a **= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is PowerAssignStatement);
            var node1 = (node0.Nodes[0] as PowerAssignStatement);
            Assert.Equal(TokenKind.PyPowerAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 7u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestFloorDivAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a //= 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is FloorDivAssignStatement);
            var node1 = (node0.Nodes[0] as FloorDivAssignStatement);
            Assert.Equal(TokenKind.PyFloorDivAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 7u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is AtomNumber);
        }
        
        [Fact]
        public void TestFloorDivAssignYieldStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a //= yield a\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is FloorDivAssignStatement);
            var node1 = (node0.Nodes[0] as FloorDivAssignStatement);
            Assert.Equal(TokenKind.PyFloorDivAssign, node1.Symbol.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 13u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.Right is YieldExpr);
        }
        
        [Fact]
        public void TestSingleAssignYieldStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a = yield a\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is AssignStatement);
            var node1 = (node0.Nodes[0] as AssignStatement);
            Assert.Equal(TokenKind.PyAssign, node1.Symbols[0].Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 11u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.RightNodes[0] is YieldExpr);
        }
        
        [Fact]
        public void TestMultipleAssignStmt()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a = b = c = 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is AssignStatement);
            var node1 = (node0.Nodes[0] as AssignStatement);
            Assert.Equal(TokenKind.PyAssign, node1.Symbols[0].Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 13u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.RightNodes[0] is TestListStarExprStatement);
            Assert.True(node1.RightNodes[1] is TestListStarExprStatement);
            Assert.True(node1.RightNodes[2] is TestListStarExprStatement);
            var node3 = (node1.RightNodes[2] as TestListStarExprStatement);
            Assert.True(node3.Nodes.Length == 1);
            Assert.True(node3.Separators.Length == 0);
            Assert.True(node3.Nodes[0] is AtomNumber);
        }
        
        [Fact]
        public void TestMultipleAssignStmtWithTypeComment()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a = b = c = 1 # type: int -> int\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is AssignStatement);
            var node1 = (node0.Nodes[0] as AssignStatement);
            Assert.Equal(TokenKind.PyAssign, node1.Symbols[0].Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 32u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node1.RightNodes[0] is TestListStarExprStatement);
            Assert.True(node1.RightNodes[1] is TestListStarExprStatement);
            Assert.True(node1.RightNodes[2] is TestListStarExprStatement);
            var node3 = (node1.RightNodes[2] as TestListStarExprStatement);
            Assert.True(node3.Nodes.Length == 1);
            Assert.True(node3.Separators.Length == 0);
            Assert.True(node3.Nodes[0] is AtomNumber);
            Assert.Equal(TokenKind.TypeComment, node1.Symbol.Kind);
        }
        
        [Fact]
        public void TestSingleAnnAssignNotAssigned()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("*a : int\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is AnnAssignStatement);
            var node1 = (node0.Nodes[0] as AnnAssignStatement);
            Assert.Equal(TokenKind.PyColon, node1.Symbol1.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 8u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is StarExpr);
            Assert.True(node1.Right is AtomName);
            Assert.True(node1.Next == null);
        }
        
        [Fact]
        public void TestSingleAnnAssign()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("*a : int = 1\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is AnnAssignStatement);
            var node1 = (node0.Nodes[0] as AnnAssignStatement);
            Assert.Equal(TokenKind.PyColon, node1.Symbol1.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 12u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is StarExpr);
            Assert.True(node1.Right is AtomName);
            Assert.True(node1.Next is TestListStarExprStatement);
            Assert.Equal(TokenKind.PyAssign, node1.Symbol2.Kind);
        }
        
        [Fact]
        public void TestSingleAnnAssignYieldExpr()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("*a : int = yield a\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is AnnAssignStatement);
            var node1 = (node0.Nodes[0] as AnnAssignStatement);
            Assert.Equal(TokenKind.PyColon, node1.Symbol1.Kind);
            Assert.True(node1.StartPos == 0u);
            Assert.True(node1.EndPos == 18u);
            Assert.True(node1.Left is TestListStarExprStatement);
            var node2 = (node1.Left as TestListStarExprStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.True(node2.Nodes[0] is StarExpr);
            Assert.True(node1.Right is AtomName);
            Assert.True(node1.Next is YieldExpr);
            Assert.Equal(TokenKind.PyAssign, node1.Symbol2.Kind);
        }
        
        [Fact]
        public void TestDelStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("del a\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is DelStatement);
            var node1 = (node0.Nodes[0] as DelStatement);
            Assert.Equal(TokenKind.PyDel, node1.Symbol.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(5u, node1.EndPos);
            Assert.True(node1.Right is AtomName);
        }
        
        [Fact]
        public void TestDelStatementWithMultipleArguments()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("del a, b, c\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is DelStatement);
            var node1 = (node0.Nodes[0] as DelStatement);
            Assert.Equal(TokenKind.PyDel, node1.Symbol.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(11u, node1.EndPos);
            Assert.True(node1.Right is ExprList);
            var node2 = (node1.Right as ExprList);
            Assert.True(node2.Nodes.Length == 3);
            Assert.True(node2.Separators.Length == 2);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node2.Nodes[1] is AtomName);
            Assert.True(node2.Nodes[2] is AtomName);
        }
        
        [Fact]
        public void TestGlobalStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("global a\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is GlobalStatement);
            var node1 = (node0.Nodes[0] as GlobalStatement);
            Assert.Equal(TokenKind.PyGlobal, node1.Symbol.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(8u, node1.EndPos);
            Assert.True(node1.Nodes.Length == 1);
            Assert.True(node1.Separators.Length == 0);
            Assert.Equal(TokenKind.Name, node1.Nodes[0].Kind);
        }
        
        [Fact]
        public void TestGlobalStatementMultipleArguments()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("global a, b, c\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is GlobalStatement);
            var node1 = (node0.Nodes[0] as GlobalStatement);
            Assert.Equal(TokenKind.PyGlobal, node1.Symbol.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(14u, node1.EndPos);
            Assert.True(node1.Nodes.Length == 3);
            Assert.True(node1.Separators.Length == 2);
            Assert.Equal(TokenKind.Name, node1.Nodes[0].Kind);
            Assert.Equal(TokenKind.Name, node1.Nodes[1].Kind);
            Assert.Equal(TokenKind.Name, node1.Nodes[2].Kind);
            Assert.Equal(TokenKind.PyComma, node1.Separators[0].Kind);
            Assert.Equal(TokenKind.PyComma, node1.Separators[1].Kind);
        }
        
        [Fact]
        public void TestNonLocalStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("nonlocal a\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is NonlocalStatement);
            var node1 = (node0.Nodes[0] as NonlocalStatement);
            Assert.Equal(TokenKind.PyNonLocal, node1.Symbol.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(10u, node1.EndPos);
            Assert.True(node1.Nodes.Length == 1);
            Assert.True(node1.Separators.Length == 0);
            Assert.Equal(TokenKind.Name, node1.Nodes[0].Kind);
        }
        
        [Fact]
        public void TestNonLocalStatementMultipleArguments()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("nonlocal a, b, c\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is NonlocalStatement);
            var node1 = (node0.Nodes[0] as NonlocalStatement);
            Assert.Equal(TokenKind.PyNonLocal, node1.Symbol.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(16u, node1.EndPos);
            Assert.True(node1.Nodes.Length == 3);
            Assert.True(node1.Separators.Length == 2);
            Assert.Equal(TokenKind.Name, node1.Nodes[0].Kind);
            Assert.Equal(TokenKind.Name, node1.Nodes[1].Kind);
            Assert.Equal(TokenKind.Name, node1.Nodes[2].Kind);
            Assert.Equal(TokenKind.PyComma, node1.Separators[0].Kind);
            Assert.Equal(TokenKind.PyComma, node1.Separators[1].Kind);
        }
        
        [Fact]
        public void TestAssertStatementSingle()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("assert a\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is AssertStatement);
            var node1 = (node0.Nodes[0] as AssertStatement);
            Assert.Equal(TokenKind.PyAssert, node1.Symbol1.Kind);
            Assert.True(node1.Symbol2 == null);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(8u, node1.EndPos);
            Assert.True(node1.Left is AtomName);
            Assert.True(node1.Right == null);
        }
        
        [Fact]
        public void TestAssertStatementMultiple()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("assert a, b\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is AssertStatement);
            var node1 = (node0.Nodes[0] as AssertStatement);
            Assert.Equal(TokenKind.PyAssert, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyComma, node1.Symbol2.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(11u, node1.EndPos);
            Assert.True(node1.Left is AtomName);
            Assert.True(node1.Right is AtomName);
        }
        
        [Fact]
        public void TestImportSingleNonDottedStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("import a\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportNameStatement);
            var node1 = (node0.Nodes[0] as ImportNameStatement);
            Assert.Equal(TokenKind.PyImport, node1.Symbol.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(8u, node1.EndPos);
            Assert.True(node1.Right is DottedNameStatement);
            var node2 = (node1.Right as DottedNameStatement);
            Assert.True(node2.Nodes.Length == 1);
            Assert.True(node2.Separators.Length == 0);
            Assert.Equal(TokenKind.Name, node2.Nodes[0].Kind);
        }
        
        [Fact]
        public void TestImportSingleDottedStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("import a.b.c\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportNameStatement);
            var node1 = (node0.Nodes[0] as ImportNameStatement);
            Assert.Equal(TokenKind.PyImport, node1.Symbol.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(12u, node1.EndPos);
            Assert.True(node1.Right is DottedNameStatement);
            var node2 = (node1.Right as DottedNameStatement);
            Assert.True(node2.Nodes.Length == 3);
            Assert.True(node2.Separators.Length == 2);
            Assert.Equal(TokenKind.Name, node2.Nodes[0].Kind);
            Assert.Equal(TokenKind.Name, node2.Nodes[1].Kind);
            Assert.Equal(TokenKind.Name, node2.Nodes[2].Kind);
            Assert.Equal(TokenKind.PyDot, node2.Separators[0].Kind);
            Assert.Equal(TokenKind.PyDot, node2.Separators[1].Kind);
        }
        
        [Fact]
        public void TestImportSingleDottedAsStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("import a.b.c as d\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportNameStatement);
            var node1 = (node0.Nodes[0] as ImportNameStatement);
            Assert.Equal(TokenKind.PyImport, node1.Symbol.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(17u, node1.EndPos);
            Assert.True(node1.Right is DottedAsNameStatement);
            var node3 = (node1.Right as DottedAsNameStatement);
            Assert.True(node3.Left is DottedNameStatement);
            Assert.Equal(TokenKind.PyAs, node3.Symbol1.Kind);
            Assert.Equal(TokenKind.Name, node3.Symbol2.Kind);
            var node2 = (node3.Left as DottedNameStatement);
            Assert.True(node2.Nodes.Length == 3);
            Assert.True(node2.Separators.Length == 2);
            Assert.Equal(TokenKind.Name, node2.Nodes[0].Kind);
            Assert.Equal(TokenKind.Name, node2.Nodes[1].Kind);
            Assert.Equal(TokenKind.Name, node2.Nodes[2].Kind);
            Assert.Equal(TokenKind.PyDot, node2.Separators[0].Kind);
            Assert.Equal(TokenKind.PyDot, node2.Separators[1].Kind);
        }
        
        [Fact]
        public void TestImportMultipleDottedAsStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("import a.b.c as d, e.f.g as h\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportNameStatement);
            var node1 = (node0.Nodes[0] as ImportNameStatement);
            Assert.Equal(TokenKind.PyImport, node1.Symbol.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(29u, node1.EndPos);
            Assert.True(node1.Right is DottedAsNamesStatement);
            var node10 = (node1.Right as DottedAsNamesStatement);
            Assert.True(node10.Nodes.Length == 2);
            Assert.True(node10.Separators.Length == 1);
            Assert.Equal(TokenKind.PyComma, node10.Separators[0].Kind);
            
            var node3 = (node10.Nodes[0] as DottedAsNameStatement);
            Assert.True(node3.Left is DottedNameStatement);
            Assert.Equal(TokenKind.PyAs, node3.Symbol1.Kind);
            Assert.Equal(TokenKind.Name, node3.Symbol2.Kind);
            var node2 = (node3.Left as DottedNameStatement);
            Assert.True(node2.Nodes.Length == 3);
            Assert.True(node2.Separators.Length == 2);
            Assert.Equal(TokenKind.Name, node2.Nodes[0].Kind);
            Assert.Equal(TokenKind.Name, node2.Nodes[1].Kind);
            Assert.Equal(TokenKind.Name, node2.Nodes[2].Kind);
            Assert.Equal(TokenKind.PyDot, node2.Separators[0].Kind);
            Assert.Equal(TokenKind.PyDot, node2.Separators[1].Kind);
            
            var node4 = (node10.Nodes[1] as DottedAsNameStatement);
            Assert.True(node4.Left is DottedNameStatement);
            Assert.Equal(TokenKind.PyAs, node4.Symbol1.Kind);
            Assert.Equal(TokenKind.Name, node4.Symbol2.Kind);
            var node5 = (node4.Left as DottedNameStatement);
            Assert.True(node5.Nodes.Length == 3);
            Assert.True(node5.Separators.Length == 2);
            Assert.Equal(TokenKind.Name, node5.Nodes[0].Kind);
            Assert.Equal(TokenKind.Name, node5.Nodes[1].Kind);
            Assert.Equal(TokenKind.Name, node5.Nodes[2].Kind);
            Assert.Equal(TokenKind.PyDot, node5.Separators[0].Kind);
            Assert.Equal(TokenKind.PyDot, node5.Separators[1].Kind);
        }
        
        [Fact]
        public void TestImportFromSingleNonDottedStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("from a import b\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportFromStatement);
            var node1 = (node0.Nodes[0] as ImportFromStatement);
            Assert.Equal(TokenKind.PyFrom, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyImport, node1.Symbol2.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(15u, node1.EndPos);
            Assert.True(node1.Dots.Length == 0);
            Assert.True(node1.Left is DottedNameStatement);
            Assert.True(node1.Right is ImportAsNameStatement);
            var node2 = (node1.Right as ImportAsNameStatement);
            Assert.Equal(TokenKind.Name, node2.Symbol1.Kind);
            Assert.True(node2.Symbol2 == null);
            Assert.True(node2.Symbol3 == null);
        }
        
        [Fact]
        public void TestImportFromSingleNonDottedStatementWithDots()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("from ....a import b\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportFromStatement);
            var node1 = (node0.Nodes[0] as ImportFromStatement);
            Assert.Equal(TokenKind.PyFrom, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyImport, node1.Symbol2.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(19u, node1.EndPos);
            Assert.True(node1.Dots.Length == 2);
            Assert.True(node1.Left is DottedNameStatement);
            Assert.True(node1.Right is ImportAsNameStatement);
            var node2 = (node1.Right as ImportAsNameStatement);
            Assert.Equal(TokenKind.Name, node2.Symbol1.Kind);
            Assert.True(node2.Symbol2 == null);
            Assert.True(node2.Symbol3 == null);
        }
        
        [Fact]
        public void TestImportFromSingleNonDottedStatementWithDotsWithoutName()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("from .... import b\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportFromStatement);
            var node1 = (node0.Nodes[0] as ImportFromStatement);
            Assert.Equal(TokenKind.PyFrom, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyImport, node1.Symbol2.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(18u, node1.EndPos);
            Assert.True(node1.Dots.Length == 2);
            Assert.True(node1.Left == null);
            Assert.True(node1.Right is ImportAsNameStatement);
            var node2 = (node1.Right as ImportAsNameStatement);
            Assert.Equal(TokenKind.Name, node2.Symbol1.Kind);
            Assert.True(node2.Symbol2 == null);
            Assert.True(node2.Symbol3 == null);
        }
        
        [Fact]
        public void TestImportFromSingleDottedStatementWithDots()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("from ....a.b import b\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportFromStatement);
            var node1 = (node0.Nodes[0] as ImportFromStatement);
            Assert.Equal(TokenKind.PyFrom, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyImport, node1.Symbol2.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(21u, node1.EndPos);
            Assert.True(node1.Dots.Length == 2);
            Assert.True(node1.Left is DottedNameStatement);
            Assert.True(node1.Right is ImportAsNameStatement);
            var node2 = (node1.Right as ImportAsNameStatement);
            Assert.Equal(TokenKind.Name, node2.Symbol1.Kind);
            Assert.True(node2.Symbol2 == null);
            Assert.True(node2.Symbol3 == null);
        }
        
        [Fact]
        public void TestImportFromSingleDottedStatementWithDotsAndStar()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("from ....a.b import *\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportFromStatement);
            var node1 = (node0.Nodes[0] as ImportFromStatement);
            Assert.Equal(TokenKind.PyFrom, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyImport, node1.Symbol2.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(21u, node1.EndPos);
            Assert.True(node1.Dots.Length == 2);
            Assert.True(node1.Left is DottedNameStatement);
            Assert.True(node1.Right is null);
            Assert.Equal(TokenKind.PyMul, node1.Symbol3.Kind);
        }
        
        [Fact]
        public void TestImportFromSingleDottedStatementWithDotsAndParen()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("from ....a.b import ( a )\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportFromStatement);
            var node1 = (node0.Nodes[0] as ImportFromStatement);
            Assert.Equal(TokenKind.PyFrom, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyImport, node1.Symbol2.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(25u, node1.EndPos);
            Assert.True(node1.Dots.Length == 2);
            Assert.True(node1.Left is DottedNameStatement);
            Assert.True(node1.Right is ImportAsNameStatement);
            var node2 = (node1.Right as ImportAsNameStatement);
            Assert.Equal(TokenKind.Name, node2.Symbol1.Kind);
            Assert.Equal(TokenKind.PyLeftParen, node1.Symbol3.Kind);
            Assert.Equal(TokenKind.PyRightParen, node1.Symbol4.Kind);
        }
        
        [Fact]
        public void TestImportFromSingleDottedStatementWithDotsAndParenAs()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("from ....a.b import ( a as b )\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportFromStatement);
            var node1 = (node0.Nodes[0] as ImportFromStatement);
            Assert.Equal(TokenKind.PyFrom, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyImport, node1.Symbol2.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(30u, node1.EndPos);
            Assert.True(node1.Dots.Length == 2);
            Assert.True(node1.Left is DottedNameStatement);
            Assert.True(node1.Right is ImportAsNameStatement);
            var node2 = (node1.Right as ImportAsNameStatement);
            Assert.Equal(TokenKind.Name, node2.Symbol1.Kind);
            Assert.Equal(TokenKind.PyAs, node2.Symbol2.Kind);
            Assert.Equal(TokenKind.Name, node2.Symbol3.Kind);
            Assert.Equal(TokenKind.PyLeftParen, node1.Symbol3.Kind);
            Assert.Equal(TokenKind.PyRightParen, node1.Symbol4.Kind);
        }
        
        [Fact]
        public void TestImportFromSingleDottedStatementWithDotsAndParenAsCommaEmpty()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("from ....a.b import ( a as b,)\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportFromStatement);
            var node1 = (node0.Nodes[0] as ImportFromStatement);
            Assert.Equal(TokenKind.PyFrom, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyImport, node1.Symbol2.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(30u, node1.EndPos);
            Assert.True(node1.Dots.Length == 2);
            Assert.True(node1.Left is DottedNameStatement);
            Assert.True(node1.Right is ImportAsNamesStatement);
            var node3 = (node1.Right as ImportAsNamesStatement);
            Assert.True(node3.Separators.Length == 1);
            Assert.Equal(TokenKind.PyComma, node3.Separators[0].Kind);
            
            Assert.True(node3.Nodes[0] is ImportAsNameStatement);
            var node2 = (node3.Nodes[0] as ImportAsNameStatement);
            Assert.Equal(TokenKind.Name, node2.Symbol1.Kind);
            Assert.Equal(TokenKind.PyAs, node2.Symbol2.Kind);
            Assert.Equal(TokenKind.Name, node2.Symbol3.Kind);
            Assert.Equal(TokenKind.PyLeftParen, node1.Symbol3.Kind);
            Assert.Equal(TokenKind.PyRightParen, node1.Symbol4.Kind);
        }
        
        [Fact]
        public void TestImportFromMultipleDottedStatementWithDotsAndParenAs()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("from ....a.b import ( a as b, c)\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportFromStatement);
            var node1 = (node0.Nodes[0] as ImportFromStatement);
            Assert.Equal(TokenKind.PyFrom, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyImport, node1.Symbol2.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(32u, node1.EndPos);
            Assert.True(node1.Dots.Length == 2);
            Assert.True(node1.Left is DottedNameStatement);
            Assert.True(node1.Right is ImportAsNamesStatement);
            var node3 = (node1.Right as ImportAsNamesStatement);
            Assert.True(node3.Separators.Length == 1);
            Assert.Equal(TokenKind.PyComma, node3.Separators[0].Kind);
            
            Assert.True(node3.Nodes[0] is ImportAsNameStatement);
            var node2 = (node3.Nodes[0] as ImportAsNameStatement);
            Assert.Equal(TokenKind.Name, node2.Symbol1.Kind);
            Assert.Equal(TokenKind.PyAs, node2.Symbol2.Kind);
            Assert.Equal(TokenKind.Name, node2.Symbol3.Kind);
            Assert.Equal(TokenKind.PyLeftParen, node1.Symbol3.Kind);
            Assert.Equal(TokenKind.PyRightParen, node1.Symbol4.Kind);
            
            Assert.True(node3.Nodes[1] is ImportAsNameStatement);
            var node4 = (node3.Nodes[1] as ImportAsNameStatement);
            Assert.Equal(TokenKind.Name, node4.Symbol1.Kind);
        }
        
        [Fact]
        public void TestImportFromSingleDottedStatementWithDotsAsCommaEmpty()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("from ....a.b import a as b,\n".ToCharArray()));
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
            Assert.True(node0.Nodes[0] is ImportFromStatement);
            var node1 = (node0.Nodes[0] as ImportFromStatement);
            Assert.Equal(TokenKind.PyFrom, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyImport, node1.Symbol2.Kind);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(27u, node1.EndPos);
            Assert.True(node1.Dots.Length == 2);
            Assert.True(node1.Left is DottedNameStatement);
            Assert.True(node1.Right is ImportAsNamesStatement);
            var node3 = (node1.Right as ImportAsNamesStatement);
            Assert.True(node3.Separators.Length == 1);
            Assert.Equal(TokenKind.PyComma, node3.Separators[0].Kind);
            
            Assert.True(node3.Nodes[0] is ImportAsNameStatement);
            var node2 = (node3.Nodes[0] as ImportAsNameStatement);
            Assert.Equal(TokenKind.Name, node2.Symbol1.Kind);
            Assert.Equal(TokenKind.PyAs, node2.Symbol2.Kind);
            Assert.Equal(TokenKind.Name, node2.Symbol3.Kind);
        }
        
        [Fact]
        public void TestCompoundIfSingleStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("if a: pass\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is IfStatement);
            var node0 = (node.Nodes[0] as IfStatement);
            Assert.Equal(0u, node0.StartPos);
            Assert.Equal(11u, node0.EndPos);
            Assert.Equal(TokenKind.PyIf, node0.Symbol1.Kind);
            Assert.True(node0.Left is AtomName);
            Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
            Assert.True(node0.Right is SimpleStatement);
        }
        
        [Fact]
        public void TestCompoundIfElseStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("if a: pass\nelse: pass\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is IfStatement);
            var node0 = (node.Nodes[0] as IfStatement);
            Assert.Equal(0u, node0.StartPos);
            Assert.Equal(22u, node0.EndPos);
            Assert.Equal(TokenKind.PyIf, node0.Symbol1.Kind);
            Assert.True(node0.Left is AtomName);
            Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
            Assert.True(node0.Right is SimpleStatement);
            Assert.True(node0.Nodes.Length == 0);
            Assert.True(node0.Next is ElseStatement);
            var node1 = (node0.Next as ElseStatement);
            Assert.Equal(TokenKind.PyElse, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node1.Symbol2.Kind);
            Assert.True(node1.Right is SimpleStatement);
        }
        
        [Fact]
        public void TestCompoundIfSingleElifElseStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("if a: pass\nelif b: pass\nelse: pass\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is IfStatement);
            var node0 = (node.Nodes[0] as IfStatement);
            Assert.Equal(0u, node0.StartPos);
            Assert.Equal(35u, node0.EndPos);
            Assert.Equal(TokenKind.PyIf, node0.Symbol1.Kind);
            Assert.True(node0.Left is AtomName);
            Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
            Assert.True(node0.Right is SimpleStatement);
            Assert.True(node0.Nodes.Length == 1);
            Assert.True(node0.Next is ElseStatement);
            var node1 = (node0.Next as ElseStatement);
            Assert.Equal(TokenKind.PyElse, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node1.Symbol2.Kind);
            Assert.True(node1.Right is SimpleStatement);
            Assert.True(node0.Nodes[0] is ElifStatement);
            var node2 = (node0.Nodes[0] as ElifStatement);
            Assert.Equal(TokenKind.PyElif, node2.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node2.Symbol2.Kind);
            Assert.True(node2.Left is AtomName);
            Assert.True(node2.Right is SimpleStatement);
        }
        
        [Fact]
        public void TestCompoundIfMultipleElifElseStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("if a: pass\nelif b: pass\nelif b: pass\nelse: pass\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is IfStatement);
            var node0 = (node.Nodes[0] as IfStatement);
            Assert.Equal(0u, node0.StartPos);
            Assert.Equal(48u, node0.EndPos);
            Assert.Equal(TokenKind.PyIf, node0.Symbol1.Kind);
            Assert.True(node0.Left is AtomName);
            Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
            Assert.True(node0.Right is SimpleStatement);
            Assert.True(node0.Nodes.Length == 2);
            Assert.True(node0.Next is ElseStatement);
            var node1 = (node0.Next as ElseStatement);
            Assert.Equal(TokenKind.PyElse, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node1.Symbol2.Kind);
            Assert.True(node1.Right is SimpleStatement);
            Assert.True(node0.Nodes[0] is ElifStatement);
            var node2 = (node0.Nodes[0] as ElifStatement);
            Assert.Equal(TokenKind.PyElif, node2.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node2.Symbol2.Kind);
            Assert.True(node2.Left is AtomName);
            Assert.True(node2.Right is SimpleStatement);
            Assert.True(node0.Nodes[1] is ElifStatement);
        }
        
        [Fact]
        public void TestCompoundSimpleWhileStatementWithBreak()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("while a := 1: break\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is WhileStatement);
            var node0 = (node.Nodes[0] as WhileStatement);
            Assert.Equal(0u, node0.StartPos);
            Assert.Equal(20u, node0.EndPos);
            Assert.Equal(TokenKind.PyWhile, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
            Assert.True(node0.Left is NamedExpr);
            Assert.True(node0.Right is SimpleStatement);
            Assert.True(node0.Next == null);
            var node1 = (node0.Right as SimpleStatement);
            Assert.True(node1.Nodes.Length == 1);
            Assert.True(node1.Nodes[0] is BreakStatement);
        }
        
        [Fact]
        public void TestCompoundSimpleWhileStatementWithContinue()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("while a := 1: continue\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is WhileStatement);
            var node0 = (node.Nodes[0] as WhileStatement);
            Assert.Equal(0u, node0.StartPos);
            Assert.Equal(23u, node0.EndPos);
            Assert.Equal(TokenKind.PyWhile, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
            Assert.True(node0.Left is NamedExpr);
            Assert.True(node0.Right is SimpleStatement);
            Assert.True(node0.Next == null);
            var node1 = (node0.Right as SimpleStatement);
            Assert.True(node1.Nodes.Length == 1);
            Assert.True(node1.Nodes[0] is ContinueStatement);
        }
        
        [Fact]
        public void TestCompoundSimpleWhileStatementWithYield()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("while a := 1: yield a\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is WhileStatement);
            var node0 = (node.Nodes[0] as WhileStatement);
            Assert.Equal(0u, node0.StartPos);
            Assert.Equal(22u, node0.EndPos);
            Assert.Equal(TokenKind.PyWhile, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
            Assert.True(node0.Left is NamedExpr);
            Assert.True(node0.Right is SimpleStatement);
            Assert.True(node0.Next == null);
            var node1 = (node0.Right as SimpleStatement);
            Assert.True(node1.Nodes.Length == 1);
            Assert.True(node1.Nodes[0] is YieldStatement);
        }
        
        [Fact]
        public void TestCompoundSimpleWhileStatementWithYieldAndElse()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("while a: yield a\nelse: pass\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is WhileStatement);
            var node0 = (node.Nodes[0] as WhileStatement);
            Assert.Equal(0u, node0.StartPos);
            Assert.Equal(28u, node0.EndPos);
            Assert.Equal(TokenKind.PyWhile, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node0.Symbol2.Kind);
            Assert.True(node0.Left is AtomName);
            Assert.True(node0.Right is SimpleStatement);
            Assert.True(node0.Next is ElseStatement);
            var node1 = (node0.Right as SimpleStatement);
            Assert.True(node1.Nodes.Length == 1);
            Assert.True(node1.Nodes[0] is YieldStatement);
        }
        
        [Fact]
        public void TestCompoundSimpleForStatementWithBreak()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("for a in b: break\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is ForStatement);
            var node0 = (node.Nodes[0] as ForStatement);
            Assert.Equal(0u, node0.StartPos);
            Assert.Equal(18u, node0.EndPos);
            Assert.Equal(TokenKind.PyFor, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyIn, node0.Symbol2.Kind);
            Assert.Equal(TokenKind.PyColon, node0.Symbol3.Kind);
            Assert.True(node0.Left is AtomName);
            Assert.True(node0.Next is SimpleStatement);
            Assert.True(node0.Right is AtomName);
            Assert.True(node0.Extra == null);
        }
        
        [Fact]
        public void TestCompoundSimpleForStatementWithBreakAndElse()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("for a, b in b, c: break\nelse: pass\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is ForStatement);
            var node0 = (node.Nodes[0] as ForStatement);
            Assert.Equal(0u, node0.StartPos);
            Assert.Equal(35u, node0.EndPos);
            Assert.Equal(TokenKind.PyFor, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyIn, node0.Symbol2.Kind);
            Assert.Equal(TokenKind.PyColon, node0.Symbol3.Kind);
            Assert.True(node0.Left is ExprList);
            Assert.True(node0.Next is SimpleStatement);
            Assert.True(node0.Right is TestList);
            Assert.True(node0.Extra is ElseStatement);
        }
        
        [Fact]
        public void TestCompoundSimpleAsyncForStatementWithBreak()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("async for a in b: break\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is AsyncStatement);
            var node1 = (node.Nodes[0] as AsyncStatement);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(24u, node1.EndPos);
            Assert.Equal(TokenKind.PyAsync, node1.Symbol.Kind);
            Assert.True(node1.Right is ForStatement);
            var node0 = (node1.Right as ForStatement);
            Assert.Equal(6u, node0.StartPos);
            Assert.Equal(24u, node0.EndPos);
            Assert.Equal(TokenKind.PyFor, node0.Symbol1.Kind);
            Assert.Equal(TokenKind.PyIn, node0.Symbol2.Kind);
            Assert.Equal(TokenKind.PyColon, node0.Symbol3.Kind);
            Assert.True(node0.Left is AtomName);
            Assert.True(node0.Next is SimpleStatement);
            Assert.True(node0.Right is AtomName);
            Assert.True(node0.Extra == null);
        }
        
        [Fact]
        public void TestCompoundSimpleClassStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("class a: break\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is ClassStatement);
            var node1 = (node.Nodes[0] as ClassStatement);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(15u, node1.EndPos);
            Assert.Equal(TokenKind.PyClass, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.Name, node1.Symbol2.Kind);
            Assert.Equal(TokenKind.PyColon, node1.Symbol5.Kind);
            Assert.True(node1.Left is null);
            Assert.True(node1.Right is SimpleStatement);
        }
        
        [Fact]
        public void TestCompoundSimpleClassStatementWithParenthesis()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("class a(): break\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is ClassStatement);
            var node1 = (node.Nodes[0] as ClassStatement);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(17u, node1.EndPos);
            Assert.Equal(TokenKind.PyClass, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.Name, node1.Symbol2.Kind);
            Assert.Equal(TokenKind.PyLeftParen, node1.Symbol3.Kind);
            Assert.Equal(TokenKind.PyRightParen, node1.Symbol4.Kind);
            Assert.Equal(TokenKind.PyColon, node1.Symbol5.Kind);
            Assert.True(node1.Left is null);
            Assert.True(node1.Right is SimpleStatement);
        }

        [Fact]
        public void TestCompoundSimpleClassStatementWithParenthesisAndArguments()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("class a(b, c): break\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is ClassStatement);
            var node1 = (node.Nodes[0] as ClassStatement);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(21u, node1.EndPos);
            Assert.Equal(TokenKind.PyClass, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.Name, node1.Symbol2.Kind);
            Assert.Equal(TokenKind.PyLeftParen, node1.Symbol3.Kind);
            Assert.Equal(TokenKind.PyRightParen, node1.Symbol4.Kind);
            Assert.Equal(TokenKind.PyColon, node1.Symbol5.Kind);
            Assert.True(node1.Left is ArgList);
            var node2 = (node1.Left as ArgList);
            Assert.True(node2.Nodes.Length == 2);
            Assert.True(node2.Separators.Length == 1);
            Assert.True(node2.Nodes[0] is AtomName);
            Assert.True(node2.Nodes[1] is AtomName);
            Assert.True(node1.Right is SimpleStatement);
        }
        
        [Fact]
        public void TestCompoundSimpleWithStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("with a: break\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is WithStatement);
            var node1 = (node.Nodes[0] as WithStatement);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(14u, node1.EndPos);
            Assert.Equal(TokenKind.PyWith, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node1.Symbol2.Kind);
            Assert.True(node1.Symbol3 == null); // TypeComment
            Assert.True(node1.Right is SimpleStatement);
            Assert.True(node1.WithItems.Length == 1);
            Assert.True(node1.WithItems[0] is WithItemStatement);
            var node2 = (node1.WithItems[0] as WithItemStatement);
            Assert.True(node2.Left is AtomName);
        }
        
        [Fact]
        public void TestCompoundSimpleWithStatementAndMoreItems()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("with a, b as c: break\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is WithStatement);
            var node1 = (node.Nodes[0] as WithStatement);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(22u, node1.EndPos);
            Assert.Equal(TokenKind.PyWith, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node1.Symbol2.Kind);
            Assert.True(node1.Symbol3 == null); // TypeComment
            Assert.True(node1.Right is SimpleStatement);
            Assert.True(node1.WithItems.Length == 2);
            Assert.True(node1.Separators.Length == 1);
            Assert.True(node1.WithItems[0] is WithItemStatement);
            var node2 = (node1.WithItems[0] as WithItemStatement);
            Assert.True(node2.Left is AtomName);
            Assert.True(node1.WithItems[1] is WithItemStatement);
            var node3 = (node1.WithItems[1] as WithItemStatement);
            Assert.True(node3.Left is AtomName);
            Assert.Equal(TokenKind.PyAs, node3.Symbol.Kind);
            Assert.True(node3.Right is AtomName);
        }
        
        [Fact]
        public void TestCompoundSimpleAsyncWithStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("async with a: break\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is AsyncStatement);
            var node1 = (node.Nodes[0] as AsyncStatement);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(20u, node1.EndPos);
            Assert.Equal(TokenKind.PyAsync, node1.Symbol.Kind);
            Assert.True(node1.Right is WithStatement);
            var node3 = (node1.Right as WithStatement);
            Assert.Equal(TokenKind.PyWith, node3.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node3.Symbol2.Kind);
            Assert.True(node3.Symbol3 == null); // TypeComment
            Assert.True(node3.Right is SimpleStatement);
            Assert.True(node3.WithItems.Length == 1);
            Assert.True(node3.WithItems[0] is WithItemStatement);
            var node2 = (node3.WithItems[0] as WithItemStatement);
            Assert.True(node2.Left is AtomName);
        }
        
        [Fact]
        public void TestCompoundSimpletryFinallyStatement()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("try: pass\nfinally: pass\n".ToCharArray()));
            var rootNode = parser.ParseFileInput();
            Assert.True(rootNode is FileInputNode);
            var node = (rootNode as FileInputNode);
            Assert.True(node.Newlines.Length == 0);
            Assert.Equal(TokenKind.EndOfFile, node.Eof.Kind);
            Assert.True(node.Nodes.Length == 1);
            Assert.True(node.Nodes[0] is TryStatement);
            var node1 = (node.Nodes[0] as TryStatement);
            Assert.Equal(0u, node1.StartPos);
            Assert.Equal(24u, node1.EndPos);
            Assert.Equal(TokenKind.PyTry, node1.Symbol1.Kind);
            Assert.Equal(TokenKind.PyColon, node1.Symbol2.Kind);
            Assert.Equal(TokenKind.PyFinally, node1.Symbol3.Kind);
            Assert.Equal(TokenKind.PyColon, node1.Symbol4.Kind);
            Assert.True(node1.Left is SimpleStatement);
            Assert.True(node1.Right is SimpleStatement);
        }
    }
}
