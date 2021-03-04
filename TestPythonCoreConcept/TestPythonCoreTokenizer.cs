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
        
        [Fact]
        public void TestOperatorOrDelimiter_Plus()
        {
            var lex = new PythonCoreTokenizer("+".ToCharArray());
            Assert.Equal(TokenKind.PyPlus, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_PlusAssign()
        {
            var lex = new PythonCoreTokenizer("+=".ToCharArray());
            Assert.Equal(TokenKind.PyPlusAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Minus()
        {
            var lex = new PythonCoreTokenizer("-".ToCharArray());
            Assert.Equal(TokenKind.PyMinus, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_MinusAssign()
        {
            var lex = new PythonCoreTokenizer("-=".ToCharArray());
            Assert.Equal(TokenKind.PyMinusAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Arrow()
        {
            var lex = new PythonCoreTokenizer("->".ToCharArray());
            Assert.Equal(TokenKind.PyArrow, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Mul()
        {
            var lex = new PythonCoreTokenizer("*".ToCharArray());
            Assert.Equal(TokenKind.PyMul, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Power()
        {
            var lex = new PythonCoreTokenizer("**".ToCharArray());
            Assert.Equal(TokenKind.PyPower, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_MulAssign()
        {
            var lex = new PythonCoreTokenizer("*=".ToCharArray());
            Assert.Equal(TokenKind.PyMulAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_PowerAssign()
        {
            var lex = new PythonCoreTokenizer("**=".ToCharArray());
            Assert.Equal(TokenKind.PyPowerAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Div()
        {
            var lex = new PythonCoreTokenizer("/".ToCharArray());
            Assert.Equal(TokenKind.PyDiv, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_FloorDiv()
        {
            var lex = new PythonCoreTokenizer("//".ToCharArray());
            Assert.Equal(TokenKind.PyFloorDiv, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_DivAssign()
        {
            var lex = new PythonCoreTokenizer("/=".ToCharArray());
            Assert.Equal(TokenKind.PyDivAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_FloorDivAssign()
        {
            var lex = new PythonCoreTokenizer("//=".ToCharArray());
            Assert.Equal(TokenKind.PyFloorDivAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Modulo()
        {
            var lex = new PythonCoreTokenizer("%".ToCharArray());
            Assert.Equal(TokenKind.PyModulo, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_ModuloAssign()
        {
            var lex = new PythonCoreTokenizer("%=".ToCharArray());
            Assert.Equal(TokenKind.PyModuloAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Matrice()
        {
            var lex = new PythonCoreTokenizer("@".ToCharArray());
            Assert.Equal(TokenKind.PyMatrice, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_MatriceAssign()
        {
            var lex = new PythonCoreTokenizer("@=".ToCharArray());
            Assert.Equal(TokenKind.PyMatriceAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Colon()
        {
            var lex = new PythonCoreTokenizer(":".ToCharArray());
            Assert.Equal(TokenKind.PyColon, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_ColonAssign()
        {
            var lex = new PythonCoreTokenizer(":=".ToCharArray());
            Assert.Equal(TokenKind.PyColonAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_()
        {
            var lex = new PythonCoreTokenizer("/".ToCharArray());
            Assert.Equal(TokenKind.PyDiv, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Less()
        {
            var lex = new PythonCoreTokenizer("<".ToCharArray());
            Assert.Equal(TokenKind.PyLess, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_LessEqual()
        {
            var lex = new PythonCoreTokenizer("<=".ToCharArray());
            Assert.Equal(TokenKind.PyLessEqual, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_NotEqualOld()
        {
            var lex = new PythonCoreTokenizer("<>".ToCharArray());
            Assert.Equal(TokenKind.PyNotEqual, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_ShiftLeft()
        {
            var lex = new PythonCoreTokenizer("<<".ToCharArray());
            Assert.Equal(TokenKind.PyShiftLeft, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_ShiftLeftAssign()
        {
            var lex = new PythonCoreTokenizer("<<=".ToCharArray());
            Assert.Equal(TokenKind.PyShiftLeftAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Greater()
        {
            var lex = new PythonCoreTokenizer(">".ToCharArray());
            Assert.Equal(TokenKind.PyGreater, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_GreaterEqual()
        {
            var lex = new PythonCoreTokenizer(">= ".ToCharArray());
            Assert.Equal(TokenKind.PyGreaterEqual, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_ShiftRight()
        {
            var lex = new PythonCoreTokenizer(">>".ToCharArray());
            Assert.Equal(TokenKind.PyShiftRight, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_ShiftRightAssign()
        {
            var lex = new PythonCoreTokenizer(">>=".ToCharArray());
            Assert.Equal(TokenKind.PyShiftRightAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Assign()
        {
            var lex = new PythonCoreTokenizer("=".ToCharArray());
            Assert.Equal(TokenKind.PyAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_Equal()
        {
            var lex = new PythonCoreTokenizer("==".ToCharArray());
            Assert.Equal(TokenKind.PyEqual, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_NotEqual()
        {
            var lex = new PythonCoreTokenizer("!=".ToCharArray());
            Assert.Equal(TokenKind.PyNotEqual, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_BitAnd()
        {
            var lex = new PythonCoreTokenizer("&".ToCharArray());
            Assert.Equal(TokenKind.PyBitAnd, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_BitAndAssign()
        {
            var lex = new PythonCoreTokenizer("&=".ToCharArray());
            Assert.Equal(TokenKind.PyBitAndAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_BitOr()
        {
            var lex = new PythonCoreTokenizer("|".ToCharArray());
            Assert.Equal(TokenKind.PyBitOr, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_BitOrAssign()
        {
            var lex = new PythonCoreTokenizer("|=".ToCharArray());
            Assert.Equal(TokenKind.PyBitOrAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_BitXor()
        {
            var lex = new PythonCoreTokenizer("^".ToCharArray());
            Assert.Equal(TokenKind.PyBitXor, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_BitXorAssign()
        {
            var lex = new PythonCoreTokenizer("^=".ToCharArray());
            Assert.Equal(TokenKind.PyBitXorAssign, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_BitInvert()
        {
            var lex = new PythonCoreTokenizer("~".ToCharArray());
            Assert.Equal(TokenKind.PyBitInvert, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_BitComma()
        {
            var lex = new PythonCoreTokenizer(",".ToCharArray());
            Assert.Equal(TokenKind.PyComma, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_BitSemiColon()
        {
            var lex = new PythonCoreTokenizer(";".ToCharArray());
            Assert.Equal(TokenKind.PySemiColon, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_BitDot()
        {
            var lex = new PythonCoreTokenizer(".".ToCharArray());
            Assert.Equal(TokenKind.PyDot, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_BitElipsis()
        {
            var lex = new PythonCoreTokenizer("...".ToCharArray());
            Assert.Equal(TokenKind.PyElipsis, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_LeftParen()
        {
            var lex = new PythonCoreTokenizer("(".ToCharArray());
            Assert.Equal(TokenKind.PyLeftParen, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_LeftBracket()
        {
            var lex = new PythonCoreTokenizer("[".ToCharArray());
            Assert.Equal(TokenKind.PyLeftBracket, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_LeftCurly()
        {
            var lex = new PythonCoreTokenizer("{".ToCharArray());
            Assert.Equal(TokenKind.PyLeftCurly, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(1ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_RightParen()
        {
            var lex = new PythonCoreTokenizer("()".ToCharArray());
            lex.Advance();
            Assert.Equal(TokenKind.PyRightParen, lex.CurSymbol.Kind);
            Assert.Equal(1ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_RightBracket()
        {
            var lex = new PythonCoreTokenizer("[]".ToCharArray());
            lex.Advance();
            Assert.Equal(TokenKind.PyRightBracket, lex.CurSymbol.Kind);
            Assert.Equal(1ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestOperatorOrDelimiter_RightCurly()
        {
            var lex = new PythonCoreTokenizer("{}".ToCharArray());
            lex.Advance();
            Assert.Equal(TokenKind.PyRightCurly, lex.CurSymbol.Kind);
            Assert.Equal(1ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
        }
        
        [Fact]
        public void TestLiteralName_1()
        {
            var lex = new PythonCoreTokenizer("Test[".ToCharArray());
            Assert.Equal(TokenKind.Name, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(4ul, lex.CurSymbol.EndPos);
            Assert.Equal("Test", (lex.CurSymbol as NameToken).Text);
        }
        
        [Fact]
        public void TestLiteralName_2()
        {
            var lex = new PythonCoreTokenizer("__init__".ToCharArray());
            Assert.Equal(TokenKind.Name, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(8ul, lex.CurSymbol.EndPos);
            Assert.Equal("__init__", (lex.CurSymbol as NameToken).Text);
        }
        
        [Fact]
        public void TestLiteralName_3()
        {
            var lex = new PythonCoreTokenizer("_x34a".ToCharArray());
            Assert.Equal(TokenKind.Name, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(5ul, lex.CurSymbol.EndPos);
            Assert.Equal("_x34a", (lex.CurSymbol as NameToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_1()
        {
            var lex = new PythonCoreTokenizer("0b_111_011".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(10ul, lex.CurSymbol.EndPos);
            Assert.Equal("0b_111_011", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_2()
        {
            var lex = new PythonCoreTokenizer("0B_111_011".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(10ul, lex.CurSymbol.EndPos);
            Assert.Equal("0B_111_011", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_3()
        {
            var lex = new PythonCoreTokenizer("".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(0ul, lex.CurSymbol.EndPos);
            Assert.Equal("", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_4()
        {
            var lex = new PythonCoreTokenizer("".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(0ul, lex.CurSymbol.EndPos);
            Assert.Equal("", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_5()
        {
            var lex = new PythonCoreTokenizer("0B111011".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(8ul, lex.CurSymbol.EndPos);
            Assert.Equal("0B111011", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_6()
        {
            var lex = new PythonCoreTokenizer("0x_7f_8e".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(8ul, lex.CurSymbol.EndPos);
            Assert.Equal("0x_7f_8e", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_7()
        {
            var lex = new PythonCoreTokenizer("0X_7F_8e".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(8ul, lex.CurSymbol.EndPos);
            Assert.Equal("0X_7F_8e", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_8()
        {
            var lex = new PythonCoreTokenizer("0X7F8e".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(6ul, lex.CurSymbol.EndPos);
            Assert.Equal("0X7F8e", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_9()
        {
            var lex = new PythonCoreTokenizer("0o_71_14".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(8ul, lex.CurSymbol.EndPos);
            Assert.Equal("0o_71_14", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_10()
        {
            var lex = new PythonCoreTokenizer("0O_71_14".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(8ul, lex.CurSymbol.EndPos);
            Assert.Equal("0O_71_14", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_11()
        {
            var lex = new PythonCoreTokenizer("0o7114".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(6ul, lex.CurSymbol.EndPos);
            Assert.Equal("0o7114", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_12()
        {
            var lex = new PythonCoreTokenizer("0.".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
            Assert.Equal("0.", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_13()
        {
            var lex = new PythonCoreTokenizer("0._0".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(4ul, lex.CurSymbol.EndPos);
            Assert.Equal("0._0", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_14()
        {
            var lex = new PythonCoreTokenizer("0._0e-34j".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(9ul, lex.CurSymbol.EndPos);
            Assert.Equal("0._0e-34j", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_15()
        {
            var lex = new PythonCoreTokenizer("0._0E-3_4J".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(10ul, lex.CurSymbol.EndPos);
            Assert.Equal("0._0E-3_4J", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_16()
        {
            var lex = new PythonCoreTokenizer("0._0E+3_4J".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(10ul, lex.CurSymbol.EndPos);
            Assert.Equal("0._0E+3_4J", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_17()
        {
            var lex = new PythonCoreTokenizer("0._0E3_4J".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(9ul, lex.CurSymbol.EndPos);
            Assert.Equal("0._0E3_4J", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_18()
        {
            var lex = new PythonCoreTokenizer("0.0J".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(4ul, lex.CurSymbol.EndPos);
            Assert.Equal("0.0J", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_19()
        {
            var lex = new PythonCoreTokenizer("0.0".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(3ul, lex.CurSymbol.EndPos);
            Assert.Equal("0.0", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_20()
        {
            var lex = new PythonCoreTokenizer(".0".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(2ul, lex.CurSymbol.EndPos);
            Assert.Equal(".0", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_21()
        {
            var lex = new PythonCoreTokenizer(".0e1j".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(5ul, lex.CurSymbol.EndPos);
            Assert.Equal(".0e1j", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_22()
        {
            var lex = new PythonCoreTokenizer(".0E1J".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(5ul, lex.CurSymbol.EndPos);
            Assert.Equal(".0E1J", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_23()
        {
            var lex = new PythonCoreTokenizer(".0E+1J".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(6ul, lex.CurSymbol.EndPos);
            Assert.Equal(".0E+1J", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_24()
        {
            var lex = new PythonCoreTokenizer(".0E-1J".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(6ul, lex.CurSymbol.EndPos);
            Assert.Equal(".0E-1J", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_25()
        {
            var lex = new PythonCoreTokenizer("1234.456".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(8ul, lex.CurSymbol.EndPos);
            Assert.Equal("1234.456", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_26()
        {
            var lex = new PythonCoreTokenizer("1_2_3_4.4_5_6".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(13ul, lex.CurSymbol.EndPos);
            Assert.Equal("1_2_3_4.4_5_6", (lex.CurSymbol as NumberToken).Text);
        }
        
        [Fact]
        public void TestLiteralNumber_27()
        {
            var lex = new PythonCoreTokenizer(".0E-1_1J".ToCharArray());
            Assert.Equal(TokenKind.Number, lex.CurSymbol.Kind);
            Assert.Equal(0ul, lex.CurSymbol.StartPos);
            Assert.Equal(8ul, lex.CurSymbol.EndPos);
            Assert.Equal(".0E-1_1J", (lex.CurSymbol as NumberToken).Text);
        }
    }
}