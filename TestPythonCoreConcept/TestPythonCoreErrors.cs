
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
    }
}