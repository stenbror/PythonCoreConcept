
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
    }
}