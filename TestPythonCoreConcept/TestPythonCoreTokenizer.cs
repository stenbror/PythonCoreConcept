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
        public void TestResrevedKeyword_class()
        {
            var lex = new PythonCoreTokenizer("class".ToCharArray());
            Assert.Equal(TokenKind.PyClass, lex.CurSymbol.Kind);
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
        
        [Fact]
        public void TestResrevedKeyword_def()
        {
            var lex = new PythonCoreTokenizer("def".ToCharArray());
            Assert.Equal(TokenKind.PyDef, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_del()
        {
            var lex = new PythonCoreTokenizer("del".ToCharArray());
            Assert.Equal(TokenKind.PyDel, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_elif()
        {
            var lex = new PythonCoreTokenizer("elif".ToCharArray());
            Assert.Equal(TokenKind.PyElif, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(4ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_else()
        {
            var lex = new PythonCoreTokenizer("else".ToCharArray());
            Assert.Equal(TokenKind.PyElse, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(4ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_except()
        {
            var lex = new PythonCoreTokenizer("except".ToCharArray());
            Assert.Equal(TokenKind.PyExcept, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(6ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_finally()
        {
            var lex = new PythonCoreTokenizer("finally".ToCharArray());
            Assert.Equal(TokenKind.PyFinally, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(7ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_for()
        {
            var lex = new PythonCoreTokenizer("for".ToCharArray());
            Assert.Equal(TokenKind.PyFor, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_from()
        {
            var lex = new PythonCoreTokenizer("from".ToCharArray());
            Assert.Equal(TokenKind.PyFrom, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(4ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_global()
        {
            var lex = new PythonCoreTokenizer("global".ToCharArray());
            Assert.Equal(TokenKind.PyGlobal, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(6ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_if()
        {
            var lex = new PythonCoreTokenizer("if".ToCharArray());
            Assert.Equal(TokenKind.PyIf, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_import()
        {
            var lex = new PythonCoreTokenizer("import".ToCharArray());
            Assert.Equal(TokenKind.PyImport, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(6ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_in()
        {
            var lex = new PythonCoreTokenizer("in".ToCharArray());
            Assert.Equal(TokenKind.PyIn, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_is()
        {
            var lex = new PythonCoreTokenizer("is".ToCharArray());
            Assert.Equal(TokenKind.PyIs, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_lambda()
        {
            var lex = new PythonCoreTokenizer("lambda".ToCharArray());
            Assert.Equal(TokenKind.PyLambda, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(6ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_nonlocal()
        {
            var lex = new PythonCoreTokenizer("nonlocal".ToCharArray());
            Assert.Equal(TokenKind.PyNonLocal, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(8ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_not()
        {
            var lex = new PythonCoreTokenizer("not".ToCharArray());
            Assert.Equal(TokenKind.PyNot, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_or()
        {
            var lex = new PythonCoreTokenizer("or".ToCharArray());
            Assert.Equal(TokenKind.PyOr, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_pass()
        {
            var lex = new PythonCoreTokenizer("pass ".ToCharArray());
            Assert.Equal(TokenKind.PyPass, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(4ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_raise()
        {
            var lex = new PythonCoreTokenizer("raise".ToCharArray());
            Assert.Equal(TokenKind.PyRaise, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(5ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_return()
        {
            var lex = new PythonCoreTokenizer("return".ToCharArray());
            Assert.Equal(TokenKind.PyReturn, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(6ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_try()
        {
            var lex = new PythonCoreTokenizer("try".ToCharArray());
            Assert.Equal(TokenKind.PyTry, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_while()
        {
            var lex = new PythonCoreTokenizer("while".ToCharArray());
            Assert.Equal(TokenKind.PyWhile, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(5ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_with()
        {
            var lex = new PythonCoreTokenizer("with".ToCharArray());
            Assert.Equal(TokenKind.PyWith, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(4ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestResrevedKeyword_yield()
        {
            var lex = new PythonCoreTokenizer("yield".ToCharArray());
            Assert.Equal(TokenKind.PyYield, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(5ul, lex.CurSymbol.EndPos);
        }
    }
}