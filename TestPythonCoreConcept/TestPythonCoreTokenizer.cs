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
    }
}