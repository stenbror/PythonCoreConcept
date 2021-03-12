
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
    }
}
