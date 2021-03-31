
using System;
using Xunit;
using PythonCoreConcept.Parser;
using PythonCoreConcept.Parser.AST;

namespace TestPythonCoreConcept
{
    public class TestPythonCoreErrors
    {
        [Fact]
        public void TestAtomError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer(".\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(0u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyDot, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestAtomTupleError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("(a\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(2u, e.Position);
                Assert.Equal("Missing ')' in literal!", e.Message);
                Assert.Equal(TokenKind.Newline, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestAtomListError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("[a\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(2u, e.Position);
                Assert.Equal("Missing ']' in literal!", e.Message);
                Assert.Equal(TokenKind.Newline, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestAtomDictionaryError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("{a\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(2u, e.Position);
                Assert.Equal("Missing '}' in literal!", e.Message);
                Assert.Equal(TokenKind.Newline, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestTrailerCallError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a(b\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(3u, e.Position);
                Assert.Equal("Expecting ')' in call!", e.Message);
                Assert.Equal(TokenKind.Newline, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestTrailerIndexError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a[b::\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(5u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.Newline, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestTrailerDotNameError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a.\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(2u, e.Position);
                Assert.Equal("Expecting NAME literal after '.'!", e.Message);
                Assert.Equal(TokenKind.Newline, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestComparisonNotMissingInError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a not b\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(6u, e.Position);
                Assert.Equal("Expecting 'not in', but missing 'in'!", e.Message);
                Assert.Equal(TokenKind.Name, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestLambdaError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(9u, e.Position);
                Assert.Equal("Missing ':' in lambda expression!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestExpressionTestError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("a if b c\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(7u, e.Position);
                Assert.Equal("Missing 'else' in test expression!", e.Message);
                Assert.Equal(TokenKind.Name, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestStarExprError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("*\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(1u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.Newline, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestDictionaryError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("{ a:b, c ,}\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(9u, e.Position);
                Assert.Equal("Missing ':' in dictionary entry!", e.Message);
                Assert.Equal(TokenKind.PyComma, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestDictionaryWrongMulError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("{ a:b, *c ,}\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(7u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyMul, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestSetWrongPowerError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("{ a, **b ,}\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(5u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyPower, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestTupleCompMissingInError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("( a for c d: pass)\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(10u, e.Position);
                Assert.Equal("Expecting 'in' in for expression!", e.Message);
                Assert.Equal(TokenKind.Name, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestTupleCompMissingForError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("( a async c in d: pass)\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(10u, e.Position);
                Assert.Equal("Expecting 'for' in for expression!", e.Message);
                Assert.Equal(TokenKind.Name, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestTupleCompMissingAfterIfError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("( a async for c in d if: pass)\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(23u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestTupleYieldMissingArgumentError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("( yield )\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(8u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestTupleYieldFromMissingArgumentError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("( yield from )\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(13u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFuncTypeMissingParenthesisError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("( -> a\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFuncTypeInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(2u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyArrow, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFuncTypeMissingArrowError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("( ) a\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFuncTypeInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(4u, e.Position);
                Assert.Equal("Expecting '->' in func type!", e.Message);
                Assert.Equal(TokenKind.Name, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFuncTypeMissingAfterMulError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("( * ) a\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFuncTypeInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(4u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFuncTypeMissingAfterPowerError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("( ** ) a\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFuncTypeInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(5u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompoundIfMissingColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("if a pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(5u, e.Position);
                Assert.Equal("Missing ':' in 'if' statement!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompoundElifMissingColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("if a: pass\nelif b pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(18u, e.Position);
                Assert.Equal("Missing ':' in 'elif' statement!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompoundElseMissingColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("if a: pass\nelif b: pass\nelse pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(29u, e.Position);
                Assert.Equal("Missing ':' in 'else' statement!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundAsyncError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("async if a: pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(6u, e.Position);
                Assert.Equal("Expecting 'def', 'with' or 'for' after 'async' statement!", e.Message);
                Assert.Equal(TokenKind.PyIf, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundWhileMissingColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("while a pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(8u, e.Position);
                Assert.Equal("Missing ':' in 'while' statement!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundForMissingInError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("for a b: pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(6u, e.Position);
                Assert.Equal("Missing 'in' in 'for' statement!", e.Message);
                Assert.Equal(TokenKind.Name, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundForMissingColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("for a in b pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(11u, e.Position);
                Assert.Equal("Missing ':' in 'for' statement!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundTryMissingColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("try pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(4u, e.Position);
                Assert.Equal("Missing ':' in 'try' statement!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFinallyMissingColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("try: pass\nfinally pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(18u, e.Position);
                Assert.Equal("Missing ':' in 'finally' statement!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundExceptMissingColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("try: pass\nexcept pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(17u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundExceptMissingAfterAsError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("try: pass\nexcept a as pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(22u, e.Position);
                Assert.Equal("Missing Name after 'as' in 'except' statement!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundWithMissingAfterAsError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("with a as: pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(9u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundWithMissingColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("with a as b pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(12u, e.Position);
                Assert.Equal("Missing ':' in 'with' statement!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundClassMissingNameError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("class: pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(5u, e.Position);
                Assert.Equal("Expecting Name of class statement!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundClassMissingRightParenthesisError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("class a(: pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(8u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundClassMissingColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("class a() pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(10u, e.Position);
                Assert.Equal("Expecting ':' in class statement!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundClassMissingItemAfterMulError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("class a(*) pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(9u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundClassMissingItemAfterPowerError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("class a(**) pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(10u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundClassMissingItemAfterAssignError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("class a(b=) pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(10u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundClassMissingItemAfterColonAssignError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("class a(b:=) pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(11u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundDecoratedError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("@class\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(1u, e.Position);
                Assert.Equal("Expecting Name literal in dotted name statement!", e.Message);
                Assert.Equal(TokenKind.PyClass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundDecoratedMissingRightParenthesisError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("@a.b(\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(5u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.Newline, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundDecoratedMissingNewlineError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("@a.b()class\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(6u, e.Position);
                Assert.Equal("Expecting Newkine after decorator!", e.Message);
                Assert.Equal(TokenKind.PyClass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundDecoratedMissingValidKeywordError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("@a.b()\nwith\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(7u, e.Position);
                Assert.Equal("Expecting 'async', 'class' or 'def' after '@' decorators!", e.Message);
                Assert.Equal(TokenKind.PyWith, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefMissingNameError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def(): pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(3u, e.Position);
                Assert.Equal("Missing Name of functiomn!", e.Message);
                Assert.Equal(TokenKind.PyLeftParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefMissingLeftParenthesisError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a: pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(5u, e.Position);
                Assert.Equal("Expecting '(' in function declaration!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefMissingRightParenthesisError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a(: pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(6u, e.Position);
                Assert.Equal("Expecting Name literal in argument!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefMissingColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a() pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(8u, e.Position);
                Assert.Equal("Expecting ':' in function declaration!", e.Message);
                Assert.Equal(TokenKind.PyPass, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefMissingAfterArrowError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a() -> : pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(11u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefArgumentMissingAfterMulError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a(*) -> c : pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(7u, e.Position);
                Assert.Equal("Missing NAME literal after '*' in argument list!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefArgumentMissingAfterPowerError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a(**) -> c : pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(8u, e.Position);
                Assert.Equal("Missing NAME literal after '**' in argument list!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefArgumentMissingAfterColonError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a(b:) -> c : pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(8u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefArgumentMissingAfterAssignError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a(b:c =) -> c : pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(11u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefArgumentMissingDivError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a(b, c, *) -> c : pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(12u, e.Position);
                Assert.Equal("Expecting ')' in function declaration!", e.Message);
                Assert.Equal(TokenKind.PyMul, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefArgumentMissingTrailingMulError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a(b, c, /, *) -> c : pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(16u, e.Position);
                Assert.Equal("Missing NAME literal after '*' in argument list!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefArgumentMissingTrailingPowerError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a(b, c, /, **) -> c : pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(17u, e.Position);
                Assert.Equal("Missing NAME literal after '**' in argument list!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputCompundFuncDefArgumentMissingTrailingMulAndPowerError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("def a(b, c, /, *d, e, **) -> c : pass\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(24u, e.Position);
                Assert.Equal("Missing NAME literal after '**' in argument list!", e.Message);
                Assert.Equal(TokenKind.PyRightParen, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputLambadVarArgsListMissingMulError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda *: b\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(8u, e.Position);
                Assert.Equal("Missing NAME literal after '*' in argument list!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputLambadVarArgsListMissingPowerError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda **: b\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(9u, e.Position);
                Assert.Equal("Missing NAME literal after '**' in argument list!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputLambadVarArgsListMissingAssignError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a=: b\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(9u, e.Position);
                Assert.Equal("Illegal literal!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputLambadVarArgsListMissingAssignFollowedByMulError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a=b, *: b\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(12u, e.Position);
                Assert.Equal("Unexpected '*' or '**' in argument list before '/'!", e.Message);
                Assert.Equal(TokenKind.PyMul, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputLambadVarArgsListFollowedByMulError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a=b, /, *: b\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(16u, e.Position);
                Assert.Equal("Missing NAME literal after '*' in argument list!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        public void TestFileInputLambadVarArgsListFollowedByPowerError()
        {
            var parser = new PythonCoreParser(new PythonCoreTokenizer("lambda a=b, /, **: b\n".ToCharArray()));
            try
            {
                var rootNode = parser.ParseFileInput();
                Assert.True(false);
            }
            catch (SyntaxError e)
            {
                Assert.Equal(17u, e.Position);
                Assert.Equal("Missing NAME literal after '**' in argument list!", e.Message);
                Assert.Equal(TokenKind.PyColon, e.Symbol.Kind);
            }
            catch 
            {
                Assert.True(false);
            }
        }
    }
}