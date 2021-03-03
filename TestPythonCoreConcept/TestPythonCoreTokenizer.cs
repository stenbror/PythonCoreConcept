using System;
using Xunit;
using PythonCoreConcept.Parser;

namespace TestPythonCoreConcept
{
    public class TestPythonCoreTokenizer
    {
        [Fact]
        public void TestResrevedKeyword_False()
        {
            var lex = new PythonCoreTokenizer("False ".ToCharArray());
            Assert.Equal(TokenKind.PyFalse, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(5ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_None()
        {
            var lex = new PythonCoreTokenizer("None ".ToCharArray());
            Assert.Equal(TokenKind.PyNone, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(4ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_True()
        {
            var lex = new PythonCoreTokenizer("True".ToCharArray());
            Assert.Equal(TokenKind.PyTrue, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(4ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_and()
        {
            var lex = new PythonCoreTokenizer("and".ToCharArray());
            Assert.Equal(TokenKind.PyAnd, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_as()
        {
            var lex = new PythonCoreTokenizer("as".ToCharArray());
            Assert.Equal(TokenKind.PyAs, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_assert()
        {
            var lex = new PythonCoreTokenizer("assert".ToCharArray());
            Assert.Equal(TokenKind.PyAssert, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(6ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_async()
        {
            var lex = new PythonCoreTokenizer("async".ToCharArray());
            Assert.Equal(TokenKind.PyAsync, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(5ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_await()
        {
            var lex = new PythonCoreTokenizer("await".ToCharArray());
            Assert.Equal(TokenKind.PyAwait, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(5ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_break()
        {
            var lex = new PythonCoreTokenizer("break".ToCharArray());
            Assert.Equal(TokenKind.PyBreak, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(5ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_continue()
        {
            var lex = new PythonCoreTokenizer("continue ".ToCharArray());
            Assert.Equal(TokenKind.PyContinue, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(8ul, lex.CurSymbol.EndPos);
        }
    }
}